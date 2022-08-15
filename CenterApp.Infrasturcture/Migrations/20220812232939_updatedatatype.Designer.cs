﻿// <auto-generated />
using System;
using CenterApp.Infrasturcture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    [DbContext(typeof(CenterDBContext))]
    [Migration("20220812232939_updatedatatype")]
    partial class updatedatatype
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CenterApp.Core.Models.Group", b =>
                {
                    b.Property<int>("Group_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Group_Id"), 1L, 1);

                    b.Property<string>("Group_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Teacher_Id")
                        .HasColumnType("int");

                    b.HasKey("Group_Id");

                    b.HasIndex("Teacher_Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Level", b =>
                {
                    b.Property<int>("Level_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Level_Id"), 1L, 1);

                    b.Property<string>("Level_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Level_Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("CenterApp.Core.Models.LevelMatrial", b =>
                {
                    b.Property<int>("Level_Id")
                        .HasColumnType("int");

                    b.Property<int>("Matrial_Id")
                        .HasColumnType("int");

                    b.HasKey("Level_Id", "Matrial_Id");

                    b.HasIndex("Matrial_Id");

                    b.ToTable("LevelMatrial");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Matrial", b =>
                {
                    b.Property<int>("Matrial_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Matrial_Id"), 1L, 1);

                    b.Property<string>("Matrial_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Matrial_Id");

                    b.ToTable("Matrials");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Stage", b =>
                {
                    b.Property<int>("Stage_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Stage_Id"), 1L, 1);

                    b.Property<int>("Level_Id")
                        .HasColumnType("int");

                    b.Property<string>("Stage_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Stage_Id");

                    b.HasIndex("Level_Id");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Student", b =>
                {
                    b.Property<int>("Student_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Student_Id"), 1L, 1);

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<int>("Stage_id")
                        .HasColumnType("int");

                    b.Property<string>("Student_Address")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("Student_BirthOfDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Student_Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Student_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Student_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Student_ParentPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Student_RegisterDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 8, 13, 1, 29, 39, 464, DateTimeKind.Local).AddTicks(3386));

                    b.Property<string>("Student_StdPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Student_Id");

                    b.HasIndex("Stage_id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CenterApp.Core.Models.StudentGroup", b =>
                {
                    b.Property<int>("Student_Id")
                        .HasColumnType("int");

                    b.Property<int>("Group_Id")
                        .HasColumnType("int");

                    b.HasKey("Student_Id", "Group_Id");

                    b.HasIndex("Group_Id");

                    b.ToTable("StudentGroup");
                });

            modelBuilder.Entity("CenterApp.Core.Models.StudentPayments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int>("Matrial_Id")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Student_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Matrial_Id");

                    b.HasIndex("Student_Id");

                    b.ToTable("StudentPayments");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Teacher", b =>
                {
                    b.Property<int>("Teacher_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Teacher_Id"), 1L, 1);

                    b.Property<DateTime>("Teacher_BirthOfDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Teacher_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Teacher_Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Teacher_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Teacher_Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Teacher_Specilist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Teacher_Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("CenterApp.Core.Models.TeacherMatrial", b =>
                {
                    b.Property<int>("Matrial_Id")
                        .HasColumnType("int");

                    b.Property<int>("Teacher_Id")
                        .HasColumnType("int");

                    b.Property<int>("Stage_Id")
                        .HasColumnType("int");

                    b.HasKey("Matrial_Id", "Teacher_Id");

                    b.HasIndex("Stage_Id");

                    b.HasIndex("Teacher_Id");

                    b.ToTable("TeacherMatrial");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Group", b =>
                {
                    b.HasOne("CenterApp.Core.Models.Teacher", "Teacher")
                        .WithMany("Groups")
                        .HasForeignKey("Teacher_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CenterApp.Core.Models.LevelMatrial", b =>
                {
                    b.HasOne("CenterApp.Core.Models.Level", "Level")
                        .WithMany("LevelMatrial")
                        .HasForeignKey("Level_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CenterApp.Core.Models.Matrial", "Matrial")
                        .WithMany("LevelMatrial")
                        .HasForeignKey("Matrial_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");

                    b.Navigation("Matrial");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Stage", b =>
                {
                    b.HasOne("CenterApp.Core.Models.Level", "Level")
                        .WithMany("Stages")
                        .HasForeignKey("Level_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Student", b =>
                {
                    b.HasOne("CenterApp.Core.Models.Stage", "Stage")
                        .WithMany("Students")
                        .HasForeignKey("Stage_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("CenterApp.Core.Models.StudentGroup", b =>
                {
                    b.HasOne("CenterApp.Core.Models.Group", "Group")
                        .WithMany("StudentGroup")
                        .HasForeignKey("Group_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CenterApp.Core.Models.Student", "Student")
                        .WithMany("StudentGroup")
                        .HasForeignKey("Student_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CenterApp.Core.Models.StudentPayments", b =>
                {
                    b.HasOne("CenterApp.Core.Models.Matrial", "Matrial")
                        .WithMany("StudentPayments")
                        .HasForeignKey("Matrial_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CenterApp.Core.Models.Student", "Student")
                        .WithMany("StudentPayments")
                        .HasForeignKey("Student_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matrial");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CenterApp.Core.Models.TeacherMatrial", b =>
                {
                    b.HasOne("CenterApp.Core.Models.Matrial", "Matrial")
                        .WithMany("TeacherMatrial")
                        .HasForeignKey("Matrial_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CenterApp.Core.Models.Stage", "Stage")
                        .WithMany("TeacherMatrial")
                        .HasForeignKey("Stage_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CenterApp.Core.Models.Teacher", "Teacher")
                        .WithMany("TeacherMatrial")
                        .HasForeignKey("Teacher_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matrial");

                    b.Navigation("Stage");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Group", b =>
                {
                    b.Navigation("StudentGroup");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Level", b =>
                {
                    b.Navigation("LevelMatrial");

                    b.Navigation("Stages");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Matrial", b =>
                {
                    b.Navigation("LevelMatrial");

                    b.Navigation("StudentPayments");

                    b.Navigation("TeacherMatrial");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Stage", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("TeacherMatrial");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Student", b =>
                {
                    b.Navigation("StudentGroup");

                    b.Navigation("StudentPayments");
                });

            modelBuilder.Entity("CenterApp.Core.Models.Teacher", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("TeacherMatrial");
                });
#pragma warning restore 612, 618
        }
    }
}
