using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public decimal MobileNumber { get; set; }

    public string ProfilePhoto { get; set; }

    public string Gender { get; set; }

    public string Password { get; set; }

    public int? RoleId { get; set; }

    public string MembershipStatus { get; set; }

    public DateOnly? MembershipExpiryDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<ContentFeedback> ContentFeedbacks { get; set; } = new List<ContentFeedback>();

    public virtual ICollection<CourseContentProgress> CourseContentProgresses { get; set; } = new List<CourseContentProgress>();

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<CourseProgress> CourseProgresses { get; set; } = new List<CourseProgress>();

    public virtual ICollection<Coursefeedback> Coursefeedbacks { get; set; } = new List<Coursefeedback>();

    public virtual ICollection<DoubtResponse> DoubtResponses { get; set; } = new List<DoubtResponse>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role Role { get; set; }

    public virtual ICollection<SaveCardDetail> SaveCardDetails { get; set; } = new List<SaveCardDetail>();

    public virtual ICollection<UserAssessmentResult> UserAssessmentResults { get; set; } = new List<UserAssessmentResult>();

    public virtual ICollection<UserBankDetail> UserBankDetails { get; set; } = new List<UserBankDetail>();

    public virtual ICollection<UserDoubt> UserDoubts { get; set; } = new List<UserDoubt>();

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
