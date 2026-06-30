using Doctor.Application.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctor.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(long id);
    Task<UserDto> CreateAsync(CreateUserDto dto);
    Task<UserDto?> UpdateAsync(long id, UpdateUserDto dto);
    Task<bool> DeleteAsync(long id);
}
