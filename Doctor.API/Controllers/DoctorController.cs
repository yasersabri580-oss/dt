using System.Security.Claims;
using Doctor.Application.DTOs.Doctor;
using Doctor.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctor.Controllers;

/// <summary>
/// Manages doctor accounts (the public-facing identity linked 1:1 to a User).
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Doctor")]
[Produces("application/json")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _service;
    public DoctorController(IDoctorService service) => _service = service;

    /// <summary>Get all doctors.</summary>
    /// <response code="200">List of all doctors.</response>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    /// <summary>Get a single doctor by ID (public — used for public profile pages).</summary>
    /// <param name="id">The doctor ID.</param>
    /// <response code="200">The doctor record, including its profile.</response>
    /// <response code="404">Doctor not found.</response>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DoctorWithProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdWithProfileAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    /// <summary>Get the doctor record (with profile) for the currently authenticated user.</summary>
    /// <remarks>
    /// Used by the authenticated doctor's own dashboard to load "my profile".
    /// </remarks>
    /// <response code="200">The caller's doctor record and profile.</response>
    /// <response code="404">No doctor record exists for this user yet.</response>
    /// <response code="401">Not authenticated.</response>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(DoctorWithProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = GetUserId();
        var item = await _service.GetByUserIdWithProfileAsync(userId);
        return item == null ? NotFound(new { message = "No doctor record found for this account." }) : Ok(item);
    }

    /// <summary>Create a doctor record for an existing user account.</summary>
    /// <remarks>
    /// **Sample request:**
    /// ```json
    /// {
    ///   "userId": 12,
    ///   "slug": { "en": "dr-john-smith", "fa": "دکتر-جان-اسمیت" },
    ///   "isActive": true
    /// }
    /// ```
    /// Each user may only have one doctor record (1:1 relationship).
    /// </remarks>
    /// <param name="dto">Doctor creation data.</param>
    /// <response code="201">Doctor created successfully.</response>
    /// <response code="400">The specified user does not exist.</response>
    /// <response code="409">A doctor record already exists for this user.</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
        {
            return Conflict(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Update a doctor's slug or active status.</summary>
    /// <param name="id">The doctor ID.</param>
    /// <param name="dto">Fields to update.</param>
    /// <response code="200">Updated doctor record.</response>
    /// <response code="404">Doctor not found.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDoctorDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    /// <summary>Delete a doctor record. Requires Admin role.</summary>
    /// <param name="id">The doctor ID.</param>
    /// <response code="204">Deleted successfully.</response>
    /// <response code="404">Doctor not found.</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    private long GetUserId()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("User ID not found in token.");
        return long.Parse(sub);
    }
}