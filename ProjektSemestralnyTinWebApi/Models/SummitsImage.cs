using System;
using System.Collections.Generic;

namespace ProjektSemestralnyTinWebApi.Models;

public partial class SummitsImage
{
    public int Id { get; set; }

    public int SummitId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string NamePl { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public virtual Summit Summit { get; set; } = null!;
}
