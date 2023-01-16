using SchoolRegister.DataAcces;
using SchoolRegister.DataAccess;
using SchoolRegister.Entities;

namespace SchoolRegister.Utility.SeedData;

public class SeedGrades
{
    public static void GradesSeed(SchoolRegisterContext context)
    {
        var students = context.Students;
        var subjects = context.Subjects;

        if (!context.Grades.Any())
        {
            var gradeOne = new GradeEntity()
            {
                DateOfIssue = DateTime.Now,
                UserId = students.FirstOrDefault().Id,
                SubjectId = subjects.FirstOrDefault().Id,
                GradeValue = GradeValue.A_Excellent
            };

            var gradeTwo = new GradeEntity()
            {
                DateOfIssue = DateTime.Now,
                UserId = students.FirstOrDefault().Id,
                SubjectId = subjects.FirstOrDefault().Id,
                GradeValue = GradeValue.B_VeryGood
            };

            context.Grades.AddRange(gradeOne, gradeTwo);
            context.SaveChanges();
        }
    }
}
