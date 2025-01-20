using System;
using System.Collections.Generic;

namespace ProjektSemestralnyTinWebApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExp { get; set; }

    public int RolesId { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Roles { get; set; } = null!;

    public virtual ICollection<PersonalSummitList> PersonalSummitLists { get; set; } = new List<PersonalSummitList>();
}
