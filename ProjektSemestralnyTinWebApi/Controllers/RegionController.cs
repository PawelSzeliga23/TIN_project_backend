using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Services;

namespace ProjektSemestralnyTinWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegionByIdAsync(int id)
        {
            try
            {
                var region = await _regionService.GetRegionAsync(id);
                return Ok(region);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Region with ID {id} not found.");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRegionsAsync()
        {
            var regions = await _regionService.GetRegionsAsync();
            return Ok(regions);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegionAsync(int id)
        {
            try
            {
                await _regionService.DeleteRegionAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Region with ID {id} not found.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync([FromBody] RegionIn? region)
        {
            if (region == null)
            {
                return BadRequest("Region data is required.");
            }

            Console.WriteLine(region.NameEn);
            Console.WriteLine(region.NamePl);
            
            var res = await _regionService.AddRegionAsync(region);
            
            if (res == null)
            {
                return BadRequest("Failed to add region.");
            }
            
            return CreatedAtAction(nameof(GetRegionById), new { id = res.Id }, res);
        }

        private IActionResult GetRegionById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
