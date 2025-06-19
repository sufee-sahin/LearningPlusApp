using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class Assessment
{
    public int AssessmentId { get; set; }

    public string CourseId { get; set; }

    public string Title { get; set; }

    public int NoOfQuestions { get; set; }

    public int TotalMarks { get; set; }

    public int PassingMarks { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<AssessmentQuestion> AssessmentQuestions { get; set; } = new List<AssessmentQuestion>();

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual Course Course { get; set; }

    public virtual ICollection<UserAssessmentResult> UserAssessmentResults { get; set; } = new List<UserAssessmentResult>();
}
