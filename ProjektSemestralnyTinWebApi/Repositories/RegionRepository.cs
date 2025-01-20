using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyTinWebApi.Context;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public class RegionRepository(MasterContext context) : IRegionRepository
{
    private readonly MasterContext _context = context;

    public async Task<RegionOut> GetRegionAsync(int id)
    {
        var region = await _context.Regions.FindAsync(id);

        if (region == null)
        {
            throw new KeyNotFoundException($"Region with id {id} not found.");
        }

        return new RegionOut()
        {
            Id = region.Id,
            NamePl = region.NamePl,
            NameEn = region.NameEn
        };
    }

    public async Task<IEnumerable<RegionOut>> GetRegionsAsync()
    {
        return await _context.Regions.Select(region => new RegionOut()
        {
            Id = region.Id,
            NamePl = region.NamePl,
            NameEn = region.NameEn
        }).ToListAsync();
    }

    public async Task DeleteRegionAsync(int id)
    {
        var region = await _context.Regions.FindAsync(id);

        if (region == null)
        {
            throw new KeyNotFoundException($"Region with id {id} not found.");
        }

        _context.Regions.Remove(region);

        await _context.SaveChangesAsync();
    }

    public async Task<Region> AddRegionAsync(RegionIn region)
    {
        var reg = new Region()
        {
            NameEn = region.NameEn,
            NamePl = region.NamePl
        };
        await _context.Regions.AddAsync(reg);
        await _context.SaveChangesAsync();

        return reg;
    }
    
    public async Task<bool> RegionExistsAsync(int regionId)
    {
        return await _context.Regions.AnyAsync(r => r.Id == regionId);
    }
}