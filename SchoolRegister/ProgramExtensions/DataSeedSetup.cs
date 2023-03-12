using Microsoft.EntityFrameworkCore;
using SchoolRegister.DataAccess;
using SchoolRegister.Utility.SeedData;
using SchoolSystem.Utility.SeedData;

namespace SchoolRegister.WebAPI.ProgramExtensions;

#nullable disable
public static class DataSeedSetup
{
    public static WebApplication SeedDefaultData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<SchoolRegisterContext>();

        // If Database is not relational skip the Seed Data part, because probably we using InMemoryDb for testing.
        if(!dbContext.Database.IsRelational())
        {
            return app;
        }

        if (app.Environment.IsDevelopment())
        {   
            SeedGroups.GroupsSeed(dbContext);
            SeedUsers.RolesSeed(dbContext);
            SeedUsers.UsersSeed(dbContext);
            SeedSubjects.SubjectsSeed(dbContext);
            SeedGrades.GradesSeed(dbContext);
        }
        else
        {
            SeedUsers.UsersSeed(dbContext);
        }

        return app;
    }
}
