using System;
using System.Collections.Generic;

namespace Tema2.Models;

public partial class CourseType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
