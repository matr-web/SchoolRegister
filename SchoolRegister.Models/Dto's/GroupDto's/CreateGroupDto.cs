using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Models.Dto_s.GroupDto_s;

public class CreateGroupDto
{
    [Required]
    public string Name { get; set; }
}
