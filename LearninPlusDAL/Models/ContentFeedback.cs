using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class ContentFeedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int ContentId { get; set; }

    public int? Rating { get; set; }

    public string Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public virtual CourseContent Content { get; set; }

    public virtual User User { get; set; }
}
