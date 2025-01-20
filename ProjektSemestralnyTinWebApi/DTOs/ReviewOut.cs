namespace ProjektSemestralnyTinWebApi.DTOs;

public class ReviewOut
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public DateTime Date { get; set; }
}