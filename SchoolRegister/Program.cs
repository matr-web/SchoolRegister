using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.BusinessAccess.Services;
using SchoolRegister.DataAcces.Repository;
using SchoolRegister.DataAcces.Repository.IRepository;
using SchoolRegister.DataAccess;
using SchoolRegister.Entities;
using SchoolRegister.Models.Models;
using SchoolRegister.Utility.SeedData;
using SchoolSystem.Utility.SeedData;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext.
builder.Services.AddDbContext<SchoolRegisterContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger Configuration
// Swagger Authentication Configuration.
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
#endregion

// Register UnitOfWork for repositories.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services.
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

#region Authentication
// Authentication configuration
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false; // We do not force only the https protocol from the client.
    cfg.SaveToken = true; // The given token should be saved on the server side for authentication.
    // Validation parameters to check if the parameters sent by the client match what the server knows.
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer, // Token Issuer.
        ValidAudience = authenticationSettings.JwtIssuer, // Which entities can use this token (same value because we generate the token within our application).
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)), // Private key generated from JwtKey value.
    };
});
#endregion

var app = builder.Build();

#region SeedData
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<SchoolRegisterContext>();
SeedGroups.GroupsSeed(dbContext);
SeedUsers.RolesSeed(dbContext);
SeedUsers.UsersSeed(dbContext);
SeedSubjects.SubjectsSeed(dbContext);
SeedGrades.GradesSeed(dbContext);
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
