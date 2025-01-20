namespace ProjektSemestralnyTinWebApi.DTOs;

public class SummitInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Height { get; set; }

    public IEnumerable<SummitImageOut> Images { get; set; } = null!;
}