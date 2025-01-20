using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyTinWebApi.Context;
using ProjektSemestralnyTinWebApi.DTOs;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public class SummitRepository(MasterContext context, IRegionRepository regionRepository) : ISummitRepository
{
    private readonly MasterContext _context = context;
    private readonly IRegionRepository _regionRepository = regionRepository;

    public async Task<SummitDetails> GetSummitDetailByIdAsync(int id)
    {
        var summitDetails = await _context.Summits
            .Where(s => s.Id == id)
            .Select(s => new SummitDetails
            {
                Id = s.Id,
                Name = s.Name,
                Height = s.Height,
                RegionId = s.RegionId,
                RegionNameEn = s.Region.NameEn,
                RegionNamePl = s.Region.NamePl,
                DescPl = s.DescPl,
                DescEn = s.DescEn,
                Images = s.SummitsImages.Select(img => new SummitImageOut
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    NameEn = img.NameEn,
                    NamePl = img.NamePl,
                }).ToList()
            }).FirstOrDefaultAsync();

        if (summitDetails == null)
        {
            throw new KeyNotFoundException($"Summit with id {id} not found.");
        }

        return summitDetails;
    }

    public async Task<IEnumerable<SummitInfo>> GetAllSummitsInfoAsync()
    {
        return await _context.Summits.Select(s => new SummitInfo()
        {
            Id = s.Id,
            Name = s.Name,
            Height = s.Height,
            Images = s.SummitsImages.Select(img => new SummitImageOut
            {
                Id = img.Id,
                ImageUrl = img.ImageUrl,
                NameEn = img.NameEn,
                NamePl = img.NamePl,
            }).ToList()
        }).ToListAsync();
    }

    public async Task AddSummitAsync(SummitIn summitIn)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var region = await _context.Regions.FindAsync(summitIn.RegionId);
            if (region == null)
            {
                throw new KeyNotFoundException($"Region with id {summitIn.RegionId} not found");
            }

            var summit = new Summit()
            {
                Name = summitIn.Name,
                Height = summitIn.Height,
                Region = region,
                DescEn = summitIn.DescEn,
                DescPl = summitIn.DescPl,
                SummitsImages = summitIn.Images.Select(i => new SummitsImage()
                {
                    ImageUrl = i.ImageUrl,
                    NameEn = i.NameEn,
                    NamePl = i.NamePl
                }).ToList()
            };

            _context.Summits.Add(summit);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error occurs during adding", ex);
        }
    }

    public async Task DeleteSummitAsync(int id)
    {
        var summit = await _context.Summits.FindAsync(id);
        if (summit == null)
        {
            throw new Exception($"Summit not found");
        }

        _context.Summits.Remove(summit);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SummitInfo>> GetAllSummitsInfoByRegionAsync(int regionId)
    {
        if (!await _regionRepository.RegionExistsAsync(regionId))
        {
            throw new Exception("Region not found");
        }

        return await _context.Summits
            .Where(s => s.RegionId == regionId)
            .Select(s => new SummitInfo()
            {
                Id = s.Id,
                Name = s.Name,
                Height = s.Height,
                Images = s.SummitsImages.Select(img => new SummitImageOut
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    NameEn = img.NameEn,
                    NamePl = img.NamePl,
                }).ToList()
            }).ToListAsync();
    }

    public async Task<Summit> GetSummitByIdAsync(int id)
    {
        var summit = await _context.Summits.FindAsync(id);
        if (summit == null)
        {
            throw new Exception("Summit not found");
        }
        return summit;
    }

    public async Task UpdateSummitAsync(SummitUpdateIn summitUpdateIn)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var summit = await _context.Summits.Include(summit => summit.SummitsImages)
                .FirstOrDefaultAsync(s => s.Id == summitUpdateIn.Id);

            if (summit == null)
            {
                throw new KeyNotFoundException($"Summit with ID {summitUpdateIn.Id} not found.");
            }
            
            summit.Name = summitUpdateIn.Name;
            summit.Height = summitUpdateIn.Height;
            summit.RegionId = summitUpdateIn.RegionId;
            summit.DescPl = summitUpdateIn.DescPl;
            summit.DescEn = summitUpdateIn.DescEn;
            
            _context.SummitsImages.RemoveRange(summit.SummitsImages);
            
            summit.SummitsImages = summitUpdateIn.Images.Select(img => new SummitsImage()
            {
                ImageUrl = img.ImageUrl,
                NameEn = img.NameEn,
                NamePl = img.NamePl,
                SummitId = summit.Id
            }).ToList();
            
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<SummitUpdateOut> GetSummitUpdateDetailsAsync(int id)
    {
        var summitDetails = await _context.Summits
            .Where(s => s.Id == id)
            .Select(s => new SummitUpdateOut()
            {
                Name = s.Name,
                Height = s.Height,
                RegionId = s.RegionId,
                DescPl = s.DescPl,
                DescEn = s.DescEn,
                Images = s.SummitsImages.Select(img => new SummitImageIn()
                {
                    ImageUrl = img.ImageUrl,
                    NameEn = img.NameEn,
                    NamePl = img.NamePl,
                }).ToList()
            }).FirstOrDefaultAsync();

        if (summitDetails == null)
        {
            throw new KeyNotFoundException($"Summit with id {id} not found.");
        }

        return summitDetails;
    }
}