namespace SchoolRegister.WebAPI.ProgramExtensions;

public static class CorsSetup
{
    public static IServiceCollection RegisterCors(this IServiceCollection services)
    {
        // CORS Configuration.
        services.AddCors(options =>
        {
            options.AddPolicy("frontEndClient", builder =>
            builder.AllowAnyMethod() // Allow any Method.
            .AllowAnyHeader() // Allow any Header.
            .WithOrigins("http://localhost:8080")); // Source for which we allow the CORS policy. 
        });

        return services;
    }
}
