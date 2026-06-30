
using AutoMapper;
using BCrypt.Net;
using Doctor.Application.DTOs.User;
using Doctor.Application.Interfaces;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;

namespace Doctor.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _uow.Users.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(long id)
    {
        var user = await _uow.Users.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        if (dto.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true
            && await _uow.Users.AdminExistsAsync())
            throw new InvalidOperationException("An Admin account already exists. Only one Admin is allowed.");

        var user = _mapper.Map<User>(dto);
        user.Role = "User";
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        await _uow.Users.AddAsync(user);
        await _uow.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateAsync(long id, UpdateUserDto dto)
    {
        var user = await _uow.Users.GetByIdAsync(id);
        if (user == null) return null;

        if (dto.Role != null)
        {
            var isPromotingToAdmin = dto.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                                     && !string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
            var isDemotingAdmin = !dto.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                                  && string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);

            if (isPromotingToAdmin && await _uow.Users.AdminExistsAsync())
                throw new InvalidOperationException("An Admin account already exists. Only one Admin is allowed.");

            if (isDemotingAdmin)
                throw new InvalidOperationException("Cannot demote the Admin account. There must always be exactly one Admin.");
        }

        _mapper.Map(dto, user);
        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        _uow.Users.Update(user);
        await _uow.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _uow.Users.GetByIdAsync(id);
        if (user == null) return false;

        if (string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot delete the Admin account. There must always be exactly one Admin.");

        _uow.Users.Delete(user);
        await _uow.SaveChangesAsync();
        return true;
    }
}
