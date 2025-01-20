namespace ProjektSemestralnyTinWebApi.DTOs;

public class SummitUpdateOut
{
    public string Name { get; set; } = null!;

    public int Height { get; set; }

    public int RegionId { get; set; }

    public string DescPl { get; set; } = null!;

    public string DescEn { get; set; } = null!;

    public IEnumerable<SummitImageIn> Images { get; set; } = null!;
}