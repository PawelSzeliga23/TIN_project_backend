using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Services;

namespace ProjektSemestralnyTinWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewsBySummitIdAsync(int id)
    {
        try
        {
            var reviews =  await _reviewService.GetReviewsBySummitIdAsync(id);
            return Ok(reviews);
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [Authorize(Roles = "user")]
    [HttpPost]
    public async Task<IActionResult> AddReviewAsync([FromBody] ReviewIn reviewIn)
    {
        try
        {
            await _reviewService.AddReviewAsync(reviewIn);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}