using SchoolRegister.Entities;

namespace SchoolRegister.Models.Dto_s.GradeDto_s;

public class CreateGradeDto
{
    public virtual GradeValue GradeValue { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }
}
