using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolRegister.Entities;

namespace SchoolRegister.DataAcces.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        // Student/Group configuration.
        builder.HasOne(s => s.Group)
       .WithMany(g => g.Students)
       .HasForeignKey(s => s.GroupId)
       .OnDelete(DeleteBehavior.ClientCascade);  // If u delete Group, Student also will be deleted.
    }
}
