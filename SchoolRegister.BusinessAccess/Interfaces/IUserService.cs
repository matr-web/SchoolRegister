using SchoolRegister.Models.Dto_s.UserDto_s;

namespace SchoolRegister.BusinessAccess.Interfaces;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<string> GenerateToken(LoginUserDto loginUserDto);
}
