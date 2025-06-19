using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class CourseProgress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string CourseId { get; set; }

    public bool IsCompleted { get; set; }

    public int ProgressPercentage { get; set; }

    public DateTime? LastAccessed { get; set; }

    public virtual Course Course { get; set; }

    public virtual User User { get; set; }
}
