using SchoolRegister.WebAPI.ProgramExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDatabase(builder);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterSwagger();

builder.Services.RegisterServices();

builder.Services.RegisterCors();

builder.Services.RegisterAuthentication(builder);

var app = builder.Build();

app.SeedDefaultData();

app.ConfigureSwagger();

app.UseCors("frontEndClient");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
