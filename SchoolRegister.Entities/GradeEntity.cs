namespace SchoolRegister.Entities;

public class GradeEntity
{
    public int Id { get; set; }

    public DateTime DateOfIssue { get; set; }

    public virtual GradeValueEntity GradeValue { get; set; }

    public Guid StudentId { get; set; }

    public virtual StudentEntity Student { get; set; }

    public int SubjectId { get; set; }

    public virtual Subject Subject { get; set; }
}
