using Microsoft.EntityFrameworkCore;
using SchoolRegister.Entities;

namespace SchoolRegister.DataAccess;

public class SchoolRegisterContext : DbContext
{
    public SchoolRegisterContext(DbContextOptions<SchoolRegisterContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<SubjectEntity> Subjects { get; set; }
    public DbSet<GroupEntity> Groups { get; set; }
    public DbSet<GradeEntity> Grades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

