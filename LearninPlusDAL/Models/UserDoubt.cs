using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class UserDoubt
{
    public int DoubtId { get; set; }

    public int UserId { get; set; }

    public string CourseId { get; set; }

    public int ContentId { get; set; }

    public string DoubtText { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CourseContent Content { get; set; }

    public virtual Course Course { get; set; }

    public virtual ICollection<DoubtResponse> DoubtResponses { get; set; } = new List<DoubtResponse>();

    public virtual User User { get; set; }
}
