namespace SchoolRegister.Entities;

public class TeacherEntity : UserEntity
{
    public virtual ICollection<SubjectEntity> Subjects { get; set; }

    public string Title { get; set; }

    public override string FullName => $"{Title}" + base.FullName;
}