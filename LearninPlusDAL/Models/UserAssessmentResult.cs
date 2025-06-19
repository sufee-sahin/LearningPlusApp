using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class UserAssessmentResult
{
    public int AssessmentResultId { get; set; }

    public int UserId { get; set; }

    public string CourseId { get; set; }

    public int AssessmentId { get; set; }

    public int? MarksObtained { get; set; }

    public string Userstatus { get; set; }

    public DateTime? AttemptDate { get; set; }

    public bool? CertificateIssued { get; set; }

    public virtual Assessment Assessment { get; set; }

    public virtual Course Course { get; set; }

    public virtual User User { get; set; }
}
