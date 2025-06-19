using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class Coursefeedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public string CourseId { get; set; }

    public int? Rating { get; set; }

    public string Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public virtual Course Course { get; set; }

    public virtual User User { get; set; }
}
