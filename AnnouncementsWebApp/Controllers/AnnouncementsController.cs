using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnnouncementsController(IAnnouncementService announcementService) : ControllerBase
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add([FromForm] AnnouncementRequestDto announcement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await announcementService.Add(announcement);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AnnouncementResponseDto>> GetById(Guid id)
    {
        ArgumentNullException.ThrowIfNull(nameof(id));
        var announcement = await announcementService.GetById(id);

        return announcement == null ? (ActionResult<AnnouncementResponseDto>)BadRequest() : (ActionResult<AnnouncementResponseDto>)Ok(announcement);
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<AnnouncementResponseDto>>> GetAll([FromQuery] string? searchTitle)
    {
        var announcements = await announcementService.GetAll(searchTitle);
        return announcements == null ? (ActionResult<IEnumerable<AnnouncementResponseDto>>)BadRequest() :(ActionResult<IEnumerable<AnnouncementResponseDto>>)Ok(announcements);

    }

    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(Guid id, [FromForm] AnnouncementRequestDto announcement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await announcementService.Update(id, announcement);
        return Ok();
    }


    [HttpDelete("{id::guid}")]

    public async Task<IActionResult> Delete(Guid id)
    {
        ArgumentNullException.ThrowIfNull(nameof(id));
        await announcementService.Delete(id);
        return NoContent();
    }

}
