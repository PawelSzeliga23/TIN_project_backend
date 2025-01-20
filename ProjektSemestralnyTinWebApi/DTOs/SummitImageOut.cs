namespace ProjektSemestralnyTinWebApi.DTOs;

public class SummitImageOut
{
    public int Id { get; set; }
    
    public string ImageUrl { get; set; } = null!;

    public string NamePl { get; set; } = null!;

    public string NameEn { get; set; } = null!;
}