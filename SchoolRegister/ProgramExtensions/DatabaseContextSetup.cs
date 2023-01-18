using Microsoft.EntityFrameworkCore;
using SchoolRegister.DataAccess;

namespace SchoolRegister.WebAPI.ProgramExtensions;

public static class DatabaseContextSetup
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Add DbContext.
        services.AddDbContext<SchoolRegisterContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        return services;
    }
}
