using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.GradeDto_s;
using SchoolRegister.Models.Dto_s.GroupDto_s;
using SchoolRegister.Models.Dto_s.RoleDto_s;

namespace SchoolRegister.Models.Dto_s.StudentDto_s;

public class StudentDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int RoleId { get; set; } 

    public RoleDto Role { get; set; }

    public virtual ICollection<GradeDto> Grades { get; set; }

    public int GroupId { get; set; }

    public virtual GroupDto Group { get; set; }

    public static StudentDto ToStudentDtoMap(StudentEntity student)
    {
        var studentDto = new StudentDto()
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            RoleId = student.RoleId,
            GroupId = student.GroupId
        };

        if(student.Role != null)
        {
            var roleDto = RoleDto.ToRoleDtoMap(student.Role);
            studentDto.Role = roleDto;
        }

        if(student.Group != null) 
        {
            var groupDto = GroupDto.ToGroupDtoMap(student.Group);
            studentDto.Group = groupDto;
        }

        if (student.Grades != null && student.Grades.Count() != 0)
        {
            var gradesDtos = new List<GradeDto>();

            foreach (var grade in student.Grades)
            {
                var gradeDto = GradeDto.ToGradeDtoMap(grade); 
                gradesDtos.Add(gradeDto);
            }

            studentDto.Grades = gradesDtos;
        }

        return studentDto;
    }
}
