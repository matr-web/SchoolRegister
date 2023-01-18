using SchoolRegister.DataAccess;
using SchoolRegister.Utility.SeedData;
using SchoolSystem.Utility.SeedData;

namespace SchoolRegister.WebAPI.ProgramExtensions;

public static class DataSeedSetup
{
    public static WebApplication SeedDefaultData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<SchoolRegisterContext>();

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
