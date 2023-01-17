using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Models.Dto_s.UserDto_s;

public class RegisterUserDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(5)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public int RoleId { get; set; }

    public int? GroupId { get; set; }

    public string? Title { get; set; }
}
