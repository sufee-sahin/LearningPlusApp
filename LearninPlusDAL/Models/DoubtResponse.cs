using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class DoubtResponse
{
    public int ResponseId { get; set; }

    public int DoubtId { get; set; }

    public int UserId { get; set; }

    public string ResponseText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual UserDoubt Doubt { get; set; }

    public virtual User User { get; set; }
}
