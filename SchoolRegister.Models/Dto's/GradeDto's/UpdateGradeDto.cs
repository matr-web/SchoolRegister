using SchoolRegister.Entities;

namespace SchoolRegister.Models.Dto_s.GradeDto_s;

public class UpdateGradeDto
{
    public int Id { get; set; }

    public virtual GradeValue GradeValue { get; set; }
}
