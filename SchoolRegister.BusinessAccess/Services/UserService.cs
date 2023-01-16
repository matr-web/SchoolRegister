using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.DataAcces.Repository.IRepository;
using SchoolRegister.Entities;
using SchoolRegister.Models.Dto_s.UserDto_s;
using SchoolRegister.Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.BusinessAccess.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public UserService(IUnitOfWork unitOfWork, IPasswordHasher<UserEntity> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var user = CreateUserBasedOnRole(registerUserDto);

        user.FirstName = registerUserDto.FirstName;
        user.LastName = registerUserDto.LastName;
        user.Email = registerUserDto.Email;
        user.RoleId = registerUserDto.RoleId;
        user.PasswordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);

        await _unitOfWork.UserRepository.AddAsync(user);

        await _unitOfWork.SaveAsync();
    }

    public async Task<string> GenerateToken(LoginUserDto loginUserDto)
    {
        // Find User, if null throw 404NotFound.
        var user = await _unitOfWork.UserRepository
            .GetByAsync(u => u.Email == loginUserDto.Email, "Role");

        if (user is null)
        {
            throw new Exception(StatusCodes.Status404NotFound.ToString());
        }

        // Check if the password provided by the user is actually the one he registered with.
        var result = _passwordHasher
            .VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception(StatusCodes.Status404NotFound.ToString());
        }

        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FullName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    #region Helpers
    private UserEntity CreateUserBasedOnRole(RegisterUserDto registerUserDto)
    {
        switch (registerUserDto.RoleId)
        {
            case 1:
                return new AdministratorEntity();
            case 2:
                return new TeacherEntity() { Title = registerUserDto.Title };
            case 3:
                return new StudentEntity() { GroupId = registerUserDto.GroupId };
            default:
                return null;
        }
    }
    #endregion
}

