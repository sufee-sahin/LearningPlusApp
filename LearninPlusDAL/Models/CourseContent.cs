using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class CourseContent
{
    public int Id { get; set; }

    public string CourseId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ContentUrl { get; set; }

    public int Sequence { get; set; }

    public bool IsFree { get; set; }

    public string NotesUrl { get; set; }

    public virtual ICollection<ContentFeedback> ContentFeedbacks { get; set; } = new List<ContentFeedback>();

    public virtual Course Course { get; set; }

    public virtual ICollection<CourseContentProgress> CourseContentProgresses { get; set; } = new List<CourseContentProgress>();

    public virtual ICollection<UserDoubt> UserDoubts { get; set; } = new List<UserDoubt>();
}
