using SchoolRegister.Entities;

namespace SchoolRegister.Models.Dto_s.SubjectDto_sl;

public class UpdateSubjectDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? TeacherId { get; set; }
}
