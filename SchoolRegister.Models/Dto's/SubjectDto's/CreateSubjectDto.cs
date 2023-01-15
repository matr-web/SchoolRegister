namespace SchoolRegister.Models.Dto_s.SubjectDto_s;

public class CreateSubjectDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? TeacherId { get; set; }
}
