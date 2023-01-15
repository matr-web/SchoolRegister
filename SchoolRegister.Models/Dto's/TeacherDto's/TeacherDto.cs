using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.RoleDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_s;

namespace SchoolRegister.Models.Dto_s.TeacherDto_s;

public class TeacherDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int RoleId { get; set; }

    public RoleDto Role { get; set; }

    public virtual ICollection<SubjectDto> Subjects { get; set; }

    public string Title { get; set; }

    public string FullName { get; set; }

    public static TeacherDto ToTeacherDtoMap(TeacherEntity teacher)
    {
        var teacherDto = new TeacherDto()
        {
            Id = teacher.Id,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            Email = teacher.Email,
            RoleId = teacher.RoleId,
            Title = teacher.Title,
            FullName = teacher.FullName
        };

        if (teacher.Role != null)
        {
            var roleDto = RoleDto.ToRoleDtoMap(teacher.Role);
            teacherDto.Role = roleDto;
        }

        if (teacher.Subjects != null && teacher.Subjects.Count() != 0)
        {
            var subjectDtos = new List<SubjectDto>();

            foreach (var subject in teacher.Subjects)
            {
                var subjectDto = SubjectDto.ToSubjectDtoMap(subject);
                subjectDtos.Add(subjectDto);
            }

            teacherDto.Subjects = subjectDtos;
        }

        return teacherDto;
    }
}
