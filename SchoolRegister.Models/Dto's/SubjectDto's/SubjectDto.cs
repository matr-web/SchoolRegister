using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.RoleDto_s;
using SchoolRegister.Models.Dto_s.TeacherDto_s;

namespace SchoolRegister.Models.Dto_s.SubjectDto_s;

public class SubjectDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int? TeacherId { get; set; }

    public virtual TeacherDto Teacher { get; set; }

    public static SubjectDto ToSubjectDtoMap(SubjectEntity subject)
    {
        var subjectDto = new SubjectDto()
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description,
            TeacherId = subject.TeacherId
        };

        if (subject.Teacher == null)
        {
            return subjectDto;
        }

        var teacherDto = new TeacherDto()
        {
            Id = subject.Teacher.Id,
            FirstName = subject.Teacher.FirstName,
            LastName = subject.Teacher.LastName,
            Email = subject.Teacher.Email,
            RoleId = subject.Teacher.RoleId,
            Title = subject.Teacher.Title,
            FullName = subject.Teacher.FullName
        };

        if (subject.Teacher.Role != null)
        {
            var roleDto = RoleDto.ToRoleDtoMap(subject.Teacher.Role);
            teacherDto.Role = roleDto;
        }

        subjectDto.Teacher = teacherDto;

        return subjectDto;
    }
}
