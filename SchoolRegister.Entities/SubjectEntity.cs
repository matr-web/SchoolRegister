namespace SchoolRegister.Entities;

public class SubjectEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? TeacherId { get; set; }

    public virtual TeacherEntity Teacher { get; set; }

    public virtual ICollection<GroupEntity> Groups { get; set; }

    public virtual ICollection<GradeEntity> Grades { get; set; }
}
