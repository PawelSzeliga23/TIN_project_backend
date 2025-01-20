using System;
using System.Collections.Generic;

namespace ProjektSemestralnyTinWebApi.Models;

public partial class Summit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Height { get; set; }

    public int RegionId { get; set; }

    public string DescPl { get; set; } = null!;

    public string DescEn { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<SummitsImage> SummitsImages { get; set; } = new List<SummitsImage>();

    public virtual ICollection<PersonalSummitList> PersonalSummitLists { get; set; } = new List<PersonalSummitList>();
}
