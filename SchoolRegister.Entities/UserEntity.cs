using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; }

    [NotMapped]
    public string Password { get; set; }

    [NotMapped]
    public virtual string FullName => $"{FirstName} {LastName}";
}

