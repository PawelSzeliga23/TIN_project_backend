using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Services;

namespace ProjektSemestralnyTinWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonalSummitListController : ControllerBase
{
    private readonly IPersonalSummitListService _personalSummitListService;

    public PersonalSummitListController(IPersonalSummitListService personalSummitListService)
    {
        _personalSummitListService = personalSummitListService;
    }

    [Authorize(Roles = "user")]
    [HttpGet]
    public async Task<IActionResult> GetSummitInfoFromPersonalListAsync()
    {
        try
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            var summits = await _personalSummitListService.GetSummitInfoFromPersonalListAsync(accessToken);
            return Ok(summits);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize(Roles = "user")]
    [HttpPost]
    public async Task<IActionResult> AddSummitToListAsync([FromBody] PersonalSummitListIn model)
    {
        try
        {
            await _personalSummitListService.AddSummitToListAsync(model);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "user")]
    [HttpDelete]
    public async Task<IActionResult> RemoveSummitFromListAsync([FromBody] PersonalSummitListIn model)
    {
        try
        {
            await _personalSummitListService.RemoveSummitFromListAsync(model);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}