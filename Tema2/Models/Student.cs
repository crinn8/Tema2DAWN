using System;
using System.Collections.Generic;

namespace Tema2.Models;

public partial class Student
{
    public int Id { get; set; }

    public int ClassId { get; set; }

    public string? FirstName { get; set; } 

    public string? LastName { get; set; }

    public string? Email { get; set; } 

    public DateTime DateOfBirth { get; set; }

    public string? Address { get; set; } 

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual User User { get; set; } = new User();

    public virtual Class Class { get; set; } = new Class();
}
