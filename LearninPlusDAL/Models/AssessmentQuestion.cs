using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class AssessmentQuestion
{
    public int QuestionId { get; set; }

    public int AssessmentId { get; set; }

    public string QuestionText { get; set; }

    public int Marks { get; set; }

    public virtual Assessment Assessment { get; set; }

    public virtual ICollection<AssessmentAnswer> AssessmentAnswers { get; set; } = new List<AssessmentAnswer>();
}
