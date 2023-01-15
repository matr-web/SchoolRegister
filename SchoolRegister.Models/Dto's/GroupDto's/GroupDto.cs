using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.StudentDto_s;
using SchoolRegister.Models.Dto_s.SubjectDto_s;

namespace SchoolRegister.Models.Dto_s.GroupDto_s;

public class GroupDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<StudentDto> Students { get; set; }

    public virtual ICollection<SubjectDto> Subjects { get; set; }

    public static GroupDto ToGroupDtoMap(GroupEntity group)
    {
        var groupDto = new GroupDto()
        {
            Id = group.Id,
            Name = group.Name,
        };

        if(group.Students != null && group.Students.Count() != 0)
        {
            var studentDtos = new List<StudentDto>();

            foreach (var student in group.Students)
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

                studentDtos.Add(studentDto);
            }

            groupDto.Students = studentDtos;
        }

        if (group.Subjects != null && group.Subjects.Count() != 0)
        {
            var subjectDtos = new List<SubjectDto>();

            foreach (var subject in group.Subjects)
            {
                var subjectDto = SubjectDto.ToSubjectDtoMap(subject);
                subjectDtos.Add(subjectDto);
            }

            groupDto.Subjects = subjectDtos;
        }

        return groupDto;
    }
}
