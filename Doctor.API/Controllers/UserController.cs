
using Doctor.Application.DTOs.User;
using Doctor.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctor.Controllers;

/// <summary>
/// Manages system user accounts (User).
/// Users are top-level accounts (distinct from Karbar/employees) with roles such as Admin or User.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
[Tags("User")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service) => _service = service;

    /// <summary>Get all system users.</summary>
    /// <response code="200">List of all users.</response>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    /// <summary>Get a specific user by ID.</summary>
    /// <param name="id">The user ID.</param>
    /// <response code="200">The user record.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("{id:long}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id)
    {
        var item = await _service.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    /// <summary>Create a new system user. Requires Admin role.</summary>
    /// <remarks>
    /// **Sample request:**
    /// ```json
    /// {
    ///   "userName": "co_leader",
    ///   "password": "Secret@1234",
    ///   "nameK": "Co-Leader Name",
    ///   "codeM": "U002"
    /// }
    /// ```
    /// New users are always created with role `"User"`. The Admin account is established
    /// automatically on first registration and cannot be created through this endpoint.
    /// </remarks>
    /// <param name="dto">User creation data.</param>
    /// <response code="201">User created successfully.</response>
    /// <response code="409">An Admin account already exists.</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdUser }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>Update an existing user. Requires Admin role.</summary>
    /// <remarks>
    /// The Admin user's role cannot be changed. No user can be promoted to Admin through this endpoint.
    /// </remarks>
    /// <param name="id">The user ID.</param>
    /// <param name="dto">Fields to update.</param>
    /// <response code="200">Updated user record.</response>
    /// <response code="400">Business rule violation (e.g., demoting the Admin).</response>
    /// <response code="404">User not found.</response>
    [HttpPut("{id:long}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateUserDto dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Delete a user. Requires Admin role.</summary>
    /// <remarks>The Admin account cannot be deleted.</remarks>
    /// <param name="id">The user ID.</param>
    /// <response code="204">Deleted successfully.</response>
    /// <response code="400">Cannot delete the Admin account.</response>
    /// <response code="404">User not found.</response>
    [HttpDelete("{id:long}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
