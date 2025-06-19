using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class UserNotification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; }
}
