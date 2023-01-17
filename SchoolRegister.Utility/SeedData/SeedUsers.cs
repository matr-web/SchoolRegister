using Microsoft.EntityFrameworkCore;
using SchoolRegister.DataAccess;
using SchoolRegister.Entities;
using SchoolRegister.Utility;

namespace SchoolSystem.Utility.SeedData;

public class SeedUsers
{
    public static void RolesSeed(SchoolRegisterContext context)
    {
        if (!context.Roles.Any())
        {
            RoleEntity administrator = new RoleEntity()
            {
                Id = 1,
                Name = StaticData.role_administrator
            };

            RoleEntity teacher = new RoleEntity()
            {
                Id = 2,
                Name = StaticData.role_teacher
            };

            RoleEntity student = new RoleEntity()
            {
                Id = 3,
                Name = StaticData.role_student
            };

            context.Database.OpenConnection();

            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles ON");

                context.Roles.AddRange(administrator, teacher, student);

                context.SaveChanges();

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }

#if DEBUG
    public static void UsersSeed(SchoolRegisterContext context)
    {
        var groups = context.Groups;

        if (!context.Users.Any())
        {
            var adminOne = new AdministratorEntity
            {
                RoleId = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "1admin@gmail.com",
            };

            var teacherOne = new TeacherEntity
            {
                RoleId = 2,
                Title = "mgr inż.",
                FirstName = "Janusz",
                LastName = "Naucz",
                Email = "1teacher@gmail.com",
            };

            var studentOne = new StudentEntity
            {
                RoleId = 3,
                GroupId = groups.FirstOrDefault().Id,
                FirstName = "Mateusz",
                LastName = "Uczen",
                Email = "1student@gmail.com",
            };

            context.Add(adminOne);
            context.Add(teacherOne);
            context.Add(studentOne);

            context.SaveChanges();
        }
    }
#else
    public static void DefaultAdminSeed(SchoolRegisterContext context)
    {
        var groups = context.Groups;

        if (!context.Users.Any())
        {
            var adminOne = new AdministratorEntity
            {
                RoleId = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "1admin@gmail.com",
            };
        }
    }
#endif
}

