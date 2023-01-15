namespace SchoolRegister.Entities;

public class GroupEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<StudentEntity> Students { get; set; }

    public virtual ICollection<SubjectEntity> Subjects { get; set; }
}
