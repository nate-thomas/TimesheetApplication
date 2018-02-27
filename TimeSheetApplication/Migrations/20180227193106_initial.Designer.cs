﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TimeSheetApplication.Data;

namespace TimeSheetApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180227193106_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.AuthorizationCodes", b =>
                {
                    b.Property<string>("AuthCode")
                        .ValueGeneratedOnAdd();

                    b.HasKey("AuthCode");

                    b.ToTable("AuthorizationCodes");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Employees", b =>
                {
                    b.Property<string>("EmployeeNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthCode");

                    b.Property<string>("EmployeeIntials");

                    b.Property<string>("FirstName");

                    b.Property<string>("Grade");

                    b.Property<string>("LastName");

                    b.Property<string>("SupervisorEmployeeNumber");

                    b.Property<string>("SupervisorNumber");

                    b.HasKey("EmployeeNumber");

                    b.HasIndex("AuthCode");

                    b.HasIndex("Grade");

                    b.HasIndex("SupervisorEmployeeNumber");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.LaborGrades", b =>
                {
                    b.Property<string>("Grade")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("PayAmount");

                    b.HasKey("Grade");

                    b.ToTable("LaborGrades");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Projects", b =>
                {
                    b.Property<string>("ProjectNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ProjectNumber");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.TimesheetRows", b =>
                {
                    b.Property<string>("EmployeeNumber");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("ProjectNumber");

                    b.Property<string>("WorkPackageNumber");

                    b.Property<double>("Friday");

                    b.Property<double>("Monday");

                    b.Property<double>("Saturday");

                    b.Property<double>("Sunday");

                    b.Property<double>("Thursday");

                    b.Property<string>("TimesheetRowsId");

                    b.Property<double>("Tuesday");

                    b.Property<double>("Wednesday");

                    b.HasKey("EmployeeNumber", "EndDate", "ProjectNumber", "WorkPackageNumber");

                    b.HasIndex("ProjectNumber", "WorkPackageNumber");

                    b.ToTable("TimesheetRows");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Timesheets", b =>
                {
                    b.Property<string>("EmployeeNumber");

                    b.Property<DateTime>("EndDate");

                    b.HasKey("EmployeeNumber", "EndDate");

                    b.ToTable("Timesheets");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.WorkPackages", b =>
                {
                    b.Property<string>("ProjectNumber");

                    b.Property<string>("WorkPackageNumber");

                    b.Property<string>("Description");

                    b.HasKey("ProjectNumber", "WorkPackageNumber");

                    b.ToTable("WorkPackages");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Employees", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.AuthorizationCodes", "AuthorizationCode")
                        .WithMany("Employees")
                        .HasForeignKey("AuthCode");

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.LaborGrades", "LaborGrade")
                        .WithMany("Employees")
                        .HasForeignKey("Grade");

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Employees", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorEmployeeNumber");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.TimesheetRows", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Timesheets", "Timesheet")
                        .WithMany("TimesheetRows")
                        .HasForeignKey("EmployeeNumber", "EndDate")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.WorkPackages", "WorkPackage")
                        .WithMany("TimesheetRows")
                        .HasForeignKey("ProjectNumber", "WorkPackageNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Timesheets", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Employees", "Employee")
                        .WithMany("Timesheets")
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.WorkPackages", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Projects", "Project")
                        .WithMany("WorkPackages")
                        .HasForeignKey("ProjectNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
