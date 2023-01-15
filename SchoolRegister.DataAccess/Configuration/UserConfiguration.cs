using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolRegister.Entities;

namespace SchoolRegister.DataAcces.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.RoleId).IsRequired();

        // User/Role configuration.
        builder.HasOne(u => u.Role)
       .WithMany(r => r.Users)
       .HasForeignKey(u => u.RoleId)
       .OnDelete(DeleteBehavior.ClientCascade); // If you delete Role, User also will be deleted.

        builder.Property(u => u.FirstName).IsRequired();

        builder.Property(u => u.LastName).IsRequired();

        builder.Property(u => u.Email).IsRequired();

        // There cannot be 2 or more same emails id DB.
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
