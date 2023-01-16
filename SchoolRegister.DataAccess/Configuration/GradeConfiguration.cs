using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolRegister.Entities;

namespace SchoolRegister.DataAcces.Configuration;

public class GradeConfiguration : IEntityTypeConfiguration<GradeEntity>
{
    public void Configure(EntityTypeBuilder<GradeEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.DateOfIssue).IsRequired();

        builder.Property(u => u.GradeValue).IsRequired();

        // Grade/Student configuration.
        builder.HasOne(g => g.Student)
       .WithMany(s => s.Grades)
       .HasForeignKey(g => g.UserId)
       .OnDelete(DeleteBehavior.ClientCascade);  // If u delete Student, Grades also will be deleted.
    }
}
