using Microsoft.AspNetCore.Identity;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.BusinessAccess.Services;
using SchoolRegister.DataAcces.Repository.IRepository;
using SchoolRegister.DataAcces.Repository;
using SchoolRegister.Entities;
using Microsoft.Extensions.Configuration;

namespace SchoolRegister.WebAPI.ProgramExtensions;

public static class DependencyInjectionSetup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        // Register UnitOfWork for repositories.
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Services.
        services.AddScoped<IGradeService, GradeService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IUserService, UserService>();

        // Register PasswordHasher.
        services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

        return services;
    }
}
