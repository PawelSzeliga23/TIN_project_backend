namespace ProjektSemestralnyTinWebApi.DTOs;

public class ReviewIn
{
    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public int SummitId { get; set; }

    public string AccessToken { get; set; } = null!;
}