namespace ProjektSemestralnyTinWebApi.Models;

public class PersonalSummitList
{
    public int SummitId { get; set; }
    public virtual Summit Summit { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
}
