using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Models.Dto_s.SubjectDto_s;

public class CreateSubjectDto
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public int? TeacherId { get; set; }
}
