using System;
using System.Collections.Generic;

namespace Tema2.Models;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
