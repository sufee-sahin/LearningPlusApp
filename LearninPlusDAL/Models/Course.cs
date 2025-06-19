using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class Course
{
    public string CourseId { get; set; }

    public string CourseName { get; set; }

    public string Description { get; set; }

    public string CourseTpye { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<CourseContentProgress> CourseContentProgresses { get; set; } = new List<CourseContentProgress>();

    public virtual ICollection<CourseContent> CourseContents { get; set; } = new List<CourseContent>();

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<CourseProgress> CourseProgresses { get; set; } = new List<CourseProgress>();

    public virtual ICollection<Coursefeedback> Coursefeedbacks { get; set; } = new List<Coursefeedback>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<UserAssessmentResult> UserAssessmentResults { get; set; } = new List<UserAssessmentResult>();

    public virtual ICollection<UserDoubt> UserDoubts { get; set; } = new List<UserDoubt>();
}
