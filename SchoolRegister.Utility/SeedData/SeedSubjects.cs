using SchoolRegister.DataAccess;
using SchoolRegister.Entities;

namespace SchoolRegister.Utility.SeedData;

public class SeedSubjects
{
#if DEBUG
    public static void SubjectsSeed(SchoolRegisterContext context)
    {
        var teachers = context.Users.OfType<TeacherEntity>();

        if (!context.Subjects.Any())
        {
            var PIGK = new SubjectEntity()
            {
                Name = "Programowanie Interaktywnej Grafiki Komputerowej",
                Description = "Przedmiot ma na celu zapoznanie studenta z dostępnymi technologiami umożliwiającymi" +
               " tworzenie interaktywnej grafiki na stronach internetowych oraz nabycie praktycznych umiejętności" +
               " projektowania oraz wykonania bogatej interaktywnej grafiki na stronach internetowych przy " +
               "wykorzystaniu wybranych narzędzi oraz technologii.",
                TeacherId = teachers.FirstOrDefault().Id,
                Groups = context.Groups.Where(g => g.Name == "Programowanie Aplikacji Internetowych").ToList()
            };

            var IO = new SubjectEntity()
            {
                Name = "Inżynieria Oprogramowania",
                Description = "Przedmiot ma za zadanie zapoznanie studentów z przebiegiem procesu produkcyjnego" +
                " oprogramowania, rozpoczynając od fazy strategicznej, poprzez ustalenie wymagań po stronie użytkownika," +
                " aż do faz końcowych, tj. testowania instalacji u użytkownika i pielęgnacji.Dzięki Inżynierii " +
                "oprogramowania studenci mają możliwość nabycia podstawowych umiejętności w zakresie projektowania oprogramowania.",
                TeacherId = teachers.FirstOrDefault().Id,
                Groups = context.Groups.Where(g => g.Name == "Inżynieria Oprogramowania").ToList()

            };

            var AISBD = new SubjectEntity()
            {
                Name = "Administrowanie internetowymi serwerami baz danych",
                Description = "Przedmiot ma na celu zapoznanie studenta z podstawowymi zadaniami " +
                "administracyjnymi serwera baz danych oraz podstawowymi usługami serwera baz danych," +
                " takimi jak: usługi raportowania, usługi integracyjne, usługi analizy oraz usługi " +
                "replikacji dla wybranego serwera baz danych. Ponadto student nabędzie praktyczne umiejętności" +
                " administrowania, zarządzania oraz wdrażania usług serwera baz danych na przykładzie wybranego " +
                "serwera baz danych.",
                TeacherId = teachers.FirstOrDefault().Id,
                Groups = context.Groups.Where(g => g.Name == "Sieci Komputerowe").ToList()
            };

            context.Subjects.AddRange(PIGK, IO, AISBD);
            context.SaveChanges();
        }
    }
#endif
}
