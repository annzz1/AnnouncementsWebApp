using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]

// ვიყენებთ dependancy injection-ს (ინტერფეისის) სერვისის კონტროლერში გამოსაყენებლად.
public class AnnouncementsController(IAnnouncementService announcementService) : ControllerBase
{
    /// <summary>
    /// ამატებს ახალ განცხადებას.
    /// </summary>
    /// <param name="announcement"> განცხადების დეტალებს გადაცვემთ ფორმის საშუალებით</param>
    /// <returns>წარმატებისას ვაბრუნებთ Ok Results, წინააღმდეგ შემთხვევაში BadRequestResult-ს ვალიდაციის შეცდომებთან ერთად.</returns>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add([FromForm] AnnouncementRequestDto announcement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await announcementService.Add(announcement);
        return Ok();
    }

    /// <summary>
    /// აბრუნებს განცხადების დეტალებს უნიკალური იდენტიფიკატორის საშუალებით.
    /// </summary>
    /// <param name="id"> უნიკალური იდენტიფიკატორი რომელიც არის Guid ტიპის.</param>
    /// <returns>წარმატებისას ვაბრუნებთ Ok Results განცხადების დეტალებლად ერთად, წინააღმდეგ შემთხვევაში BadRequestResult-ს (მაშინ როდესაც მეთოდი null-ს გვიბრუნებს).</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AnnouncementDetailsResponseDto>> GetById(Guid id)
    {
        ArgumentNullException.ThrowIfNull(nameof(id));
        var announcement = await announcementService.GetById(id);

        return announcement == null ? (ActionResult<AnnouncementDetailsResponseDto>)BadRequest() : (ActionResult<AnnouncementDetailsResponseDto>)Ok(announcement);
    }

    /// <summary>
    /// აბრუნებს განცხადებების სიას, შესაძლოა გადაცემული იყოს სათაური ფილტრაციისთვის.
    /// </summary>
    /// <param name="searchTitle">არასავალდებულო პარამეტრი, ძებნის და ფილტრაციის ოპერაციისთვის. </param>
    /// <returns>წარმატებისას ვაბრუნებთ Ok Results განცხადებებთან ერთად, წინააღმდეგ შემთხვევაში BadRequestResult-ს (მაშინ როდესაც მეთოდი null-ს გვიბრუნებს).</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseDto>>> GetAll([FromQuery] string? searchTitle)
    {
        var announcements = await announcementService.GetAll(searchTitle);
        return announcements == null ? (ActionResult<IEnumerable<AnnouncementResponseDto>>)BadRequest() : (ActionResult<IEnumerable<AnnouncementResponseDto>>)Ok(announcements);
    }

    /// <summary>
    /// განცხადების რედაქტირების/განახლების ფუნქცია
    /// </summary>
    /// <param name="id">უნიკალური იდენტიფიკატორი რომლის საშულებითაც ვარკვევთ რომელი განცხადება განვაახლოთ.არის Guid ტიპის.</param>
    /// <param name="announcement">განცხადების არსებულ ველები ფორმის საშუალებით გადაეცემა ვებ გვერდს.</param>
    /// <returns>წარმატებისას ვაბრუნებთ Ok Results, წინააღმდეგ შემთხვევაში BadRequestResult-ს ვალიდაციის შეცდომებთან ერთად.</returns>
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

    /// <summary>
    /// წაშლის ფუნქცია უნიკალური იდენტიფიკატორით.
    /// </summary>
    /// <param name="id">Tუნიკალური იდენტიფიკატორი, რომლის საშულებითაც ვარკვევთ რომელი განცხადება წავშალოთ.არის Guid ტიპის.</param>
    /// <returns>A NoContent result if successful.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        ArgumentNullException.ThrowIfNull(nameof(id));
        await announcementService.Delete(id);
        return NoContent();
    }
}
