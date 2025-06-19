using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class CourseEnrollment
{
    public int EnrollmentId { get; set; }

    public int UserId { get; set; }

    public string CourseId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public DateOnly? ValidUntil { get; set; }

    public string Status { get; set; }

    public virtual Course Course { get; set; }

    public virtual User User { get; set; }
}
