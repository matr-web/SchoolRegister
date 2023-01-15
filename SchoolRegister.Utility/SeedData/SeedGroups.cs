using SchoolRegister.DataAccess;
using SchoolRegister.Entities;

namespace SchoolSystem.Utility.SeedData;

public class SeedGroups
{
    public static void GroupsSeed(SchoolRegisterContext context)
    {
        if (!context.Groups.Any())
        {
            var PAI = new GroupEntity()
            {
                Name = "Programowanie Aplikacji Internetowych"
            };

            var IO = new GroupEntity()
            {
                Name = "Inżynieria Oprogramowania"
            };

            var SK = new GroupEntity()
            {
                Name = "Sieci Komputerowe"
            };

            context.AddRange(PAI, IO, SK);
            context.SaveChanges();
        }
    }
}
