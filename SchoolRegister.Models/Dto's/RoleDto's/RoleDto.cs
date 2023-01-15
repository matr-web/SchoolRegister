using SchoolRegister.Entities;

namespace SchoolRegister.Models.Dto_s.RoleDto_s;

public class RoleDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public static RoleDto ToRoleDtoMap(RoleEntity role)
    {
        return new RoleDto()
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}
