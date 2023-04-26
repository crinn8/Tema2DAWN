using System;
using System.Collections.Generic;

namespace Tema2.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int CourseTypeId { get; set; }

    public double Value { get; set; }

    public virtual CourseType CourseType { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
