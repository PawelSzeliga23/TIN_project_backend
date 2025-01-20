using System;
using System.Collections.Generic;

namespace ProjektSemestralnyTinWebApi.Models;

public partial class Region
{
    public int Id { get; set; }

    public string NamePl { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public virtual ICollection<Summit> Summits { get; set; } = new List<Summit>();
}
