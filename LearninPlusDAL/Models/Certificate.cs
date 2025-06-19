using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public int UserId { get; set; }

    public string CourseId { get; set; }

    public int AssessmentId { get; set; }

    public string CertificateUrl { get; set; }

    public DateTime? IssueDate { get; set; }

    public int PercentageObtained { get; set; }

    public string IssuedBy { get; set; }

    public virtual Assessment Assessment { get; set; }

    public virtual Course Course { get; set; }

    public virtual User User { get; set; }
}
