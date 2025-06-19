using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class AssessmentAnswer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public string OptionText { get; set; }

    public bool IsCorrect { get; set; }

    public virtual AssessmentQuestion Question { get; set; }
}
