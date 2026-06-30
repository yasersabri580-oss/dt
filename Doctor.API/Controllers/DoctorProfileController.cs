using System.Security.Claims;
using Doctor.Application.DTOs.DoctorProfile;
using Doctor.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctor.Controllers;

/// <summary>
/// Manages the rich profile content (hero copy, SEO fields, mission, etc.) for a doctor.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("DoctorProfile")]
[Produces("application/json")]
public class DoctorProfileController : ControllerBase
{
    private readonly IDoctorProfileService _service;
    private readonly IDoctorService _doctorService;

    public DoctorProfileController(IDoctorProfileService service, IDoctorService doctorService)
    {
        _service = service;
        _doctorService = doctorService;
    }

    /// <summary>Get a doctor's profile by doctor ID (public).</summary>
    /// <param name="doctorId">The doctor ID.</param>
    /// <response code="200">The profile record.</response>
    /// <response code="404">Profile not found.</response>
    [HttpGet("{doctorId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DoctorProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByDoctorId(Guid doctorId)
    {
        var item = await _service.GetByDoctorIdAsync(doctorId);
        return item == null ? NotFound() : Ok(item);
    }

    /// <summary>Get the profile belonging to the currently authenticated doctor.</summary>
    /// <response code="200">The caller's profile.</response>
    /// <response code="404">No doctor or profile exists for this account.</response>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(DoctorProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = GetUserId();
        var doctor = await _doctorService.GetByUserIdWithProfileAsync(userId);
        if (doctor == null) return NotFound(new { message = "No doctor record found for this account." });
        return doctor.Profile == null
            ? NotFound(new { message = "No profile has been created yet for this doctor." })
            : Ok(doctor.Profile);
    }

    /// <summary>Create the profile for a doctor (one profile per doctor).</summary>
    /// <remarks>
    /// **Sample request (abridged):**
    /// ```json
    /// {
    ///   "doctorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///   "experienceYears": 12,
    ///   "fullName": { "en": "Dr. John Smith", "fa": "دکتر جان اسمیت" },
    ///   "heroTitle": { "en": "Caring for your smile", "fa": "..." },
    ///   "heroCopy": { "en": "...", "fa": "..." },
    ///   "primaryCta": { "en": "Book Appointment", "fa": "..." },
    ///   "secondaryCta": { "en": "Learn More", "fa": "..." }
    /// }
    /// ```
    /// All localized fields are required but may contain an empty string per locale.
    /// </remarks>
    /// <param name="dto">Profile content.</param>
    /// <response code="201">Profile created successfully.</response>
    /// <response code="400">The specified doctor does not exist.</response>
    /// <response code="409">A profile already exists for this doctor.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(DoctorProfileDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateDoctorProfileDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByDoctorId), new { doctorId = created.DoctorId }, created);
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

    /// <summary>Update fields of a doctor's profile. Only provided fields are changed.</summary>
    /// <param name="doctorId">The doctor ID.</param>
    /// <param name="dto">Fields to update.</param>
    /// <response code="200">Updated profile.</response>
    /// <response code="404">Profile not found.</response>
    [HttpPut("{doctorId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(DoctorProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid doctorId, [FromBody] UpdateDoctorProfileDto dto)
    {
        var updated = await _service.UpdateAsync(doctorId, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    /// <summary>Delete a doctor's profile.</summary>
    /// <param name="doctorId">The doctor ID.</param>
    /// <response code="204">Deleted successfully.</response>
    /// <response code="404">Profile not found.</response>
    [HttpDelete("{doctorId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid doctorId)
    {
        var deleted = await _service.DeleteAsync(doctorId);
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