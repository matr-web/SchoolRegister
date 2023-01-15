using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolRegister.Entities;

namespace SchoolRegister.DataAcces.Configuration;

public class SubjectConfiguration : IEntityTypeConfiguration<SubjectEntity>
{
    public void Configure(EntityTypeBuilder<SubjectEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired();

        // Subject/Group configuration.
        builder.HasMany(s => s.Groups)
            .WithMany(g => g.Subjects).UsingEntity<Dictionary<string, object>>(
            "SubjectGroup",
            x => x.HasOne<GroupEntity>().WithMany().OnDelete(DeleteBehavior.ClientCascade),
            x => x.HasOne<SubjectEntity>().WithMany().OnDelete(DeleteBehavior.ClientCascade) 
        ).ToTable("SubjectGroup");

        // Subject/Grades configuration.
        builder.HasMany(s => s.Grades)
            .WithOne(g => g.Subject)
            .HasForeignKey(g => g.SubjectId)
            .OnDelete(DeleteBehavior.ClientCascade); // If you delete Subject, Grades will also be deleted.      
    }
}
