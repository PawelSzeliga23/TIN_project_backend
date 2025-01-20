using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Services;

namespace ProjektSemestralnyTinWebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SummitController : ControllerBase
{
    private readonly ISummitService _summitService;

    public SummitController(ISummitService summitService)
    {
        _summitService = summitService;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSummitDetailsAsync(int id)
    {
        try
        {
            var summit = await _summitService.GetSummitDetailByIdAsync(id);
            return Ok(summit);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllSummitsInfoAsync()
    {
        var summits = await _summitService.GetAllSummitsInfoAsync();
        return Ok(summits);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("region")]
    public async Task<IActionResult> GetAllSummitsInfoByRegionAsync([FromQuery] int regionId)
    {
        try
        {
            var summits = await _summitService.GetAllSummitsInfoByRegionAsync(regionId);
            return Ok(summits);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> AddSummitAsync([FromBody] SummitIn? summitIn)
    {
        if (summitIn == null)
        {
            return BadRequest("Region data is required.");
        }

        try
        {
            await _summitService.AddSummitAsync(summitIn);
            return Created();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSummitAsync(int id)
    {
        try
        {
            await _summitService.DeleteSummitAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSummitAsync(int id, [FromBody] SummitIn summitIn)
    {
        var summitUpdateIn = new SummitUpdateIn()
        {
            Id = id,
            Name = summitIn.Name,
            Height = summitIn.Height,
            RegionId = summitIn.RegionId,
            DescEn = summitIn.DescEn,
            DescPl = summitIn.DescPl,
            Images = summitIn.Images
        };
        try
        {
            await _summitService.UpdateSummitAsync(summitUpdateIn);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("update/{id}")]
    public async Task<IActionResult> GetSummitUpdateDetailsAsync(int id)
    {
        try
        {
            var summit = await _summitService.GetSummitUpdateDetailsAsync(id);
            return Ok(summit);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}