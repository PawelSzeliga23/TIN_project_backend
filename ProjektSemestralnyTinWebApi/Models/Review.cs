using System;
using System.Collections.Generic;

namespace ProjektSemestralnyTinWebApi.Models;

public partial class Review
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public int UserId { get; set; }

    public int SummitId { get; set; }

    public DateTime Date { get; set; }

    public virtual Summit Summit { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
