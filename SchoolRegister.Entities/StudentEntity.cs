namespace SchoolRegister.Entities;

public class StudentEntity : UserEntity
{
    public virtual ICollection<GradeEntity> Grades { get; set; }

    public int? GroupId { get; set; }

    public virtual GroupEntity Group { get; set; }
}
