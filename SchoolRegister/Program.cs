using Microsoft.EntityFrameworkCore;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.BusinessAccess.Services;
using SchoolRegister.DataAcces.Repository;
using SchoolRegister.DataAcces.Repository.IRepository;
using SchoolRegister.DataAccess;
using SchoolRegister.Utility.SeedData;
using SchoolSystem.Utility.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext.
builder.Services.AddDbContext<SchoolRegisterContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register UnitOfWork for repositories.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services.
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
