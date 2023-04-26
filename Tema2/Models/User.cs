using System;
using System.Collections.Generic;

namespace Tema2.Models;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual Role? Role { get; set; } 

    public virtual Student? Student { get; set; }
}
