using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Models.Dto_s.UserDto_s;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(5)]
    public string Password { get; set; }
}
