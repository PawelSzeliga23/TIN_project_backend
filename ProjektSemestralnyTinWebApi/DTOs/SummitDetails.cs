namespace ProjektSemestralnyTinWebApi.DTOs;

public class SummitDetails
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Height { get; set; }

    public int RegionId { get; set; }

    public string RegionNameEn { get; set; } = null!;
    public string RegionNamePl { get; set; } = null!;

    public string DescPl { get; set; } = null!;

    public string DescEn { get; set; } = null!;
    
    public IEnumerable<SummitImageOut> Images { get; set; } = null!;
}