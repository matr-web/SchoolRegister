namespace SchoolRegister.Entities;

public class GradeEntity
{
    public int Id { get; set; }

    public DateTime DateOfIssue { get; set; }

    public virtual GradeValue GradeValue { get; set; }

    public Guid UserId { get; set; }

    public virtual StudentEntity Student { get; set; }

    public int SubjectId { get; set; }

    public virtual SubjectEntity Subject { get; set; }
}
