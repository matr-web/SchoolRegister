﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolRegister.DataAccess;

#nullable disable

namespace SchoolRegister.DataAccess.Migrations
{
    [DbContext(typeof(SchoolRegisterContext))]
    partial class SchoolRegisterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SchoolRegister.Entities.GradeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<int>("GradeValue")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("SchoolRegister.Entities.GroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SchoolRegister.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SchoolRegister.Entities.SubjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SchoolRegister.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserEntity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SubjectGroup", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("SubjectGroup", (string)null);
                });

            modelBuilder.Entity("SchoolRegister.Entities.AdministratorEntity", b =>
                {
                    b.HasBaseType("SchoolRegister.Entities.UserEntity");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("SchoolRegister.Entities.StudentEntity", b =>
                {
                    b.HasBaseType("SchoolRegister.Entities.UserEntity");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasIndex("GroupId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("SchoolRegister.Entities.TeacherEntity", b =>
                {
                    b.HasBaseType("SchoolRegister.Entities.UserEntity");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("SchoolRegister.Entities.GradeEntity", b =>
                {
                    b.HasOne("SchoolRegister.Entities.SubjectEntity", "Subject")
                        .WithMany("Grades")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SchoolRegister.Entities.StudentEntity", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolRegister.Entities.SubjectEntity", b =>
                {
                    b.HasOne("SchoolRegister.Entities.TeacherEntity", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SchoolRegister.Entities.UserEntity", b =>
                {
                    b.HasOne("SchoolRegister.Entities.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SubjectGroup", b =>
                {
                    b.HasOne("SchoolRegister.Entities.GroupEntity", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SchoolRegister.Entities.SubjectEntity", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolRegister.Entities.StudentEntity", b =>
                {
                    b.HasOne("SchoolRegister.Entities.GroupEntity", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SchoolRegister.Entities.GroupEntity", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("SchoolRegister.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SchoolRegister.Entities.SubjectEntity", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("SchoolRegister.Entities.StudentEntity", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("SchoolRegister.Entities.TeacherEntity", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
