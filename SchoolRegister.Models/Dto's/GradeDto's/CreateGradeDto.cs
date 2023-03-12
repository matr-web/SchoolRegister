using SchoolRegister.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolRegister.Models.Dto_s.GradeDto_s;

public class CreateGradeDto
{
    [Required]
    public virtual GradeValue GradeValue { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int StudentId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int SubjectId { get; set; }
}
