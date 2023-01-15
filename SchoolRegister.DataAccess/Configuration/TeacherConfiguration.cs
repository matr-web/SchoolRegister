using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolRegister.Entities;

namespace SchoolRegister.DataAcces.Configuration;

public class TeacherConfiguration : IEntityTypeConfiguration<TeacherEntity>
{
    public void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        // Teacher/Subjects configuration.
        builder.HasMany(t => t.Subjects)
            .WithOne(q => q.Teacher)
            .HasForeignKey(q => q.TeacherId)
            .OnDelete(DeleteBehavior.NoAction); // If you delete Teacher, Subjects will remain in the db.
    }
}
