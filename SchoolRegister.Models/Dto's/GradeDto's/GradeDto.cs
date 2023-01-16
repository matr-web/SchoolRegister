using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.StudentDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_s;

namespace SchoolRegister.Models.Dto_s.GradeDto_s;

public class GradeDto
{
    public int Id { get; set; }

    public DateTime DateOfIssue { get; set; }

    public virtual GradeValue GradeValue { get; set; }

    public Guid StudentId { get; set; }

    public virtual StudentDto Student { get; set; }

    public int SubjectId { get; set; }

    public virtual SubjectDto Subject { get; set; }

    public static GradeDto ToGradeDtoMap(GradeEntity grade)
    {
        var gradeDto = new GradeDto()
        {
            Id = grade.Id,
            DateOfIssue = grade.DateOfIssue,
            GradeValue = grade.GradeValue,
            StudentId = grade.UserId,
            SubjectId = grade.SubjectId,
        };

        if (grade.Subject != null)
        {
            var subjectDto = SubjectDto.ToSubjectDtoMap(grade.Subject);

            gradeDto.Subject = subjectDto;
        }

        if (grade.Student != null) 
        {
            grade.Student.Grades = null; 

            var studentDto = StudentDto.ToStudentDtoMap(grade.Student);
            gradeDto.Student = studentDto;
        }

        return gradeDto;
    }
}
