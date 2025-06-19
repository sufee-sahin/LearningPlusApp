using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LearninPlusDAL.Models;

public partial class LearningPlusContext : DbContext
{
    public LearningPlusContext()
    {
    }

    public LearningPlusContext(DbContextOptions<LearningPlusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assessment> Assessments { get; set; }

    public virtual DbSet<AssessmentAnswer> AssessmentAnswers { get; set; }

    public virtual DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<ContentFeedback> ContentFeedbacks { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseContent> CourseContents { get; set; }

    public virtual DbSet<CourseContentProgress> CourseContentProgresses { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<CourseProgress> CourseProgresses { get; set; }

    public virtual DbSet<Coursefeedback> Coursefeedbacks { get; set; }

    public virtual DbSet<DoubtResponse> DoubtResponses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SaveCardDetail> SaveCardDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAssessmentResult> UserAssessmentResults { get; set; }

    public virtual DbSet<UserBankDetail> UserBankDetails { get; set; }

    public virtual DbSet<UserDoubt> UserDoubts { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =(localdb)\\MSSQLLocalDB;Initial Catalog=learningPlus;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.HasKey(e => e.AssessmentId).HasName("PK__assessme__C7742772394AB3C2");

            entity.ToTable("assessment");

            entity.Property(e => e.AssessmentId).HasColumnName("assessmentId");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.NoOfQuestions).HasColumnName("noOfQuestions");
            entity.Property(e => e.PassingMarks).HasColumnName("passingMarks");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.TotalMarks).HasColumnName("totalMarks");

            entity.HasOne(d => d.Course).WithMany(p => p.Assessments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_assessment_course");
        });

        modelBuilder.Entity<AssessmentAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__assessme__6836B974B5B37739");

            entity.ToTable("assessmentAnswers");

            entity.Property(e => e.AnswerId).HasColumnName("answerId");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.OptionText)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("optionText");
            entity.Property(e => e.QuestionId).HasColumnName("questionId");

            entity.HasOne(d => d.Question).WithMany(p => p.AssessmentAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("fk_ques");
        });

        modelBuilder.Entity<AssessmentQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__assessme__6238D4B222F890DD");

            entity.ToTable("assessmentQuestions");

            entity.Property(e => e.QuestionId).HasColumnName("questionId");
            entity.Property(e => e.AssessmentId).HasColumnName("assessmentId");
            entity.Property(e => e.Marks).HasColumnName("marks");
            entity.Property(e => e.QuestionText)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("questionText");

            entity.HasOne(d => d.Assessment).WithMany(p => p.AssessmentQuestions)
                .HasForeignKey(d => d.AssessmentId)
                .HasConstraintName("fk_assessmentQuestions_assessment");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__certific__A15CBEAE30C76536");

            entity.ToTable("certificates");

            entity.Property(e => e.CertificateId).HasColumnName("certificateId");
            entity.Property(e => e.AssessmentId).HasColumnName("assessmentId");
            entity.Property(e => e.CertificateUrl)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("certificateUrl");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.IssueDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("issueDate");
            entity.Property(e => e.IssuedBy)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("issuedBy");
            entity.Property(e => e.PercentageObtained).HasColumnName("percentageObtained");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Assessment).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_certificate_assessment");

            entity.HasOne(d => d.Course).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_certificate_course");

            entity.HasOne(d => d.User).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_certificate_user");
        });

        modelBuilder.Entity<ContentFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__contentF__2613FD24B4F57B6F");

            entity.ToTable("contentFeedbacks");

            entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.ContentId).HasColumnName("contentId");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("feedbackDate");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Content).WithMany(p => p.ContentFeedbacks)
                .HasForeignKey(d => d.ContentId)
                .HasConstraintName("fk_contentFeedback_content");

            entity.HasOne(d => d.User).WithMany(p => p.ContentFeedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_contentFeedback_user");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__courses__2AA84FD178062691");

            entity.ToTable("courses");

            entity.Property(e => e.CourseId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.CourseName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("courseName");
            entity.Property(e => e.CourseTpye)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("free")
                .HasColumnName("courseTpye");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<CourseContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__courseCo__3213E83F41DAAE01");

            entity.ToTable("courseContent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContentUrl)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("contentUrl");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IsFree).HasColumnName("isFree");
            entity.Property(e => e.NotesUrl)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("notesUrl");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseContents)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_courseContent_course");
        });

        modelBuilder.Entity<CourseContentProgress>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ContentId }).HasName("pk_courseContentProgress");

            entity.ToTable("courseContentProgress");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ContentId).HasColumnName("contentId");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.IsCompleted).HasColumnName("isCompleted");
            entity.Property(e => e.LastAccessed)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("lastAccessed");
            entity.Property(e => e.ProgressPercentage).HasColumnName("progressPercentage");

            entity.HasOne(d => d.Content).WithMany(p => p.CourseContentProgresses)
                .HasForeignKey(d => d.ContentId)
                .HasConstraintName("fk_courseContentProgress_content");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseContentProgresses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_courseContentProgress_course");

            entity.HasOne(d => d.User).WithMany(p => p.CourseContentProgresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_courseContentProgress_user");
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__courseEn__ACFF2C86D75BE324");

            entity.ToTable("courseEnrollments");

            entity.Property(e => e.EnrollmentId).HasColumnName("enrollmentId");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("enrollmentDate");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ValidUntil).HasColumnName("validUntil");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_courseEnrollment_course");

            entity.HasOne(d => d.User).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_courseEnrollment_user");
        });

        modelBuilder.Entity<CourseProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__coursePr__3213E83F78676D5E");

            entity.ToTable("courseProgress");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.IsCompleted).HasColumnName("isCompleted");
            entity.Property(e => e.LastAccessed)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("lastAccessed");
            entity.Property(e => e.ProgressPercentage).HasColumnName("progressPercentage");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseProgresses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_courseProgress_course");

            entity.HasOne(d => d.User).WithMany(p => p.CourseProgresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_courseProgress_user");
        });

        modelBuilder.Entity<Coursefeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Coursefe__2613FD2438499BF5");

            entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("feedbackDate");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Course).WithMany(p => p.Coursefeedbacks)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_feedback_course");

            entity.HasOne(d => d.User).WithMany(p => p.Coursefeedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_feedback_user");
        });

        modelBuilder.Entity<DoubtResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK__doubtRes__0C2BB631759FE4C9");

            entity.ToTable("doubtResponses");

            entity.Property(e => e.ResponseId).HasColumnName("responseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DoubtId).HasColumnName("doubtId");
            entity.Property(e => e.ResponseText)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("responseText");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Doubt).WithMany(p => p.DoubtResponses)
                .HasForeignKey(d => d.DoubtId)
                .HasConstraintName("fk_response_doubt");

            entity.HasOne(d => d.User).WithMany(p => p.DoubtResponses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_response_user");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__A0D9EFC68519DAF5");

            entity.Property(e => e.PaymentId).HasColumnName("paymentId");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("paymentDate");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("paymentStatus");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Course).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_payment_course");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_payment_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FD20E1082");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SaveCardDetail>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__saveCard__4D5BC491276A3874");

            entity.ToTable("saveCardDetails");

            entity.HasIndex(e => e.AccountNumber, "UQ__saveCard__17D0878A6BE72E51").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("cardId");
            entity.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("accountNumber");
            entity.Property(e => e.CardHolderName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cardHolderName");
            entity.Property(e => e.CardNumber)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cardNumber");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Cvv).HasColumnName("cvv");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiryDate");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("isDefault");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.SaveCardDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_card_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__CB9A1CFFA4A26011");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.MembershipExpiryDate).HasColumnName("membership_expiryDate");
            entity.Property(e => e.MembershipStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("inactive")
                .HasColumnName("membershipStatus");
            entity.Property(e => e.MobileNumber)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("mobileNumber");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePhoto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("profilePhoto");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Role");
        });

        modelBuilder.Entity<UserAssessmentResult>(entity =>
        {
            entity.HasKey(e => e.AssessmentResultId).HasName("PK__userAsse__DB84DA8CB0C65176");

            entity.ToTable("userAssessmentResults");

            entity.Property(e => e.AssessmentResultId).HasColumnName("assessmentResultId");
            entity.Property(e => e.AssessmentId).HasColumnName("assessmentId");
            entity.Property(e => e.AttemptDate)
                .HasColumnType("datetime")
                .HasColumnName("attemptDate");
            entity.Property(e => e.CertificateIssued)
                .HasDefaultValue(false)
                .HasColumnName("certificateIssued");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.MarksObtained).HasColumnName("marksObtained");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Userstatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("not attemped");

            entity.HasOne(d => d.Assessment).WithMany(p => p.UserAssessmentResults)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("us_assesmentId");

            entity.HasOne(d => d.Course).WithMany(p => p.UserAssessmentResults)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("us_courseId");

            entity.HasOne(d => d.User).WithMany(p => p.UserAssessmentResults)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("us_userId");
        });

        modelBuilder.Entity<UserBankDetail>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PK__UserBank__E710188CD4952204");

            entity.HasIndex(e => e.AccountNumber, "UQ__UserBank__17D0878AAB4F19CF").IsUnique();

            entity.Property(e => e.BankId).HasColumnName("bankId");
            entity.Property(e => e.AccountHolderName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("accountHolderName");
            entity.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("accountNumber");
            entity.Property(e => e.AvailableBalance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("availableBalance");
            entity.Property(e => e.BankName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bankName");
            entity.Property(e => e.IfscCode)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ifscCode");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("isDefault");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserBankDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_bank_user");
        });

        modelBuilder.Entity<UserDoubt>(entity =>
        {
            entity.HasKey(e => e.DoubtId).HasName("PK__userDoub__D12B9F58341D2E4D");

            entity.ToTable("userDoubts");

            entity.Property(e => e.DoubtId).HasColumnName("doubtId");
            entity.Property(e => e.ContentId).HasColumnName("contentId");
            entity.Property(e => e.CourseId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DoubtText)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("doubtText");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Content).WithMany(p => p.UserDoubts)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doubt_content");

            entity.HasOne(d => d.Course).WithMany(p => p.UserDoubts)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_doubt_course");

            entity.HasOne(d => d.User).WithMany(p => p.UserDoubts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_doubt_user");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__userNoti__4BA5CEA9E0307598");

            entity.ToTable("userNotifications");

            entity.Property(e => e.NotificationId).HasColumnName("notificationId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("isRead");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_notification_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
