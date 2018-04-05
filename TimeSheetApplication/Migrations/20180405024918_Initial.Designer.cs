﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TimeSheetApplication.Data;

namespace TimeSheetApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180405024918_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId")
                        .IsRequired();

                    b.Property<string>("ClientSecret");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("ConsentType");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Permissions");

                    b.Property<string>("PostLogoutRedirectUris");

                    b.Property<string>("Properties");

                    b.Property<string>("RedirectUris");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("Properties");

                    b.Property<string>("Scopes");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Properties");

                    b.Property<string>("Resources");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset?>("CreationDate");

                    b.Property<DateTimeOffset?>("ExpirationDate");

                    b.Property<string>("Payload");

                    b.Property<string>("Properties");

                    b.Property<string>("ReferenceId");

                    b.Property<string>("Status");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.HasIndex("ReferenceId")
                        .IsUnique()
                        .HasFilter("[ReferenceId] IS NOT NULL");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("EmployeeNumber");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeNumber");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Employee", b =>
                {
                    b.Property<string>("EmployeeNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmployeeIntials");

                    b.Property<string>("FirstName");

                    b.Property<string>("Grade");

                    b.Property<string>("LastName");

                    b.Property<string>("SupervisorNumber");

                    b.HasKey("EmployeeNumber");

                    b.HasIndex("Grade");

                    b.HasIndex("SupervisorNumber");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.LaborGrade", b =>
                {
                    b.Property<string>("Grade")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("PayAmount");

                    b.HasKey("Grade");

                    b.ToTable("LaborGrades");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Project", b =>
                {
                    b.Property<string>("ProjectNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Budget");

                    b.Property<string>("Description");

                    b.Property<string>("StatusName");

                    b.HasKey("ProjectNumber");

                    b.HasIndex("StatusName");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.ProjectStatus", b =>
                {
                    b.Property<string>("StatusName")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("StatusName");

                    b.ToTable("ProjectStatus");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.ProjectTeam", b =>
                {
                    b.Property<string>("EmployeeNumber");

                    b.Property<string>("ProjectNumber");

                    b.HasKey("EmployeeNumber", "ProjectNumber");

                    b.HasIndex("ProjectNumber");

                    b.ToTable("ProjectTeams");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.REBbyGrade", b =>
                {
                    b.Property<string>("ProjectNumber");

                    b.Property<string>("WorkPackageNumber");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Grade");

                    b.Property<int>("EstimatedManHours");

                    b.HasKey("ProjectNumber", "WorkPackageNumber", "EndDate", "Grade");

                    b.HasIndex("Grade");

                    b.ToTable("REBbyGrades");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.ResponsibleEngineerBudget", b =>
                {
                    b.Property<string>("ProjectNumber");

                    b.Property<string>("WorkPackageNumber");

                    b.Property<DateTime>("EndDate");

                    b.HasKey("ProjectNumber", "WorkPackageNumber", "EndDate");

                    b.ToTable("ResponsibleEngineerBudgets");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Timesheet", b =>
                {
                    b.Property<string>("EmployeeNumber");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("StatusName");

                    b.HasKey("EmployeeNumber", "EndDate");

                    b.HasIndex("StatusName");

                    b.ToTable("Timesheets");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.TimesheetRow", b =>
                {
                    b.Property<string>("EmployeeNumber");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("ProjectNumber");

                    b.Property<string>("WorkPackageNumber");

                    b.Property<double>("Friday");

                    b.Property<double>("Monday");

                    b.Property<string>("Notes");

                    b.Property<double>("Saturday");

                    b.Property<double>("Sunday");

                    b.Property<double>("Thursday");

                    b.Property<double>("Tuesday");

                    b.Property<double>("Wednesday");

                    b.HasKey("EmployeeNumber", "EndDate", "ProjectNumber", "WorkPackageNumber");

                    b.HasIndex("ProjectNumber", "WorkPackageNumber");

                    b.ToTable("TimesheetRows");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.TimesheetStatus", b =>
                {
                    b.Property<string>("StatusName")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("StatusName");

                    b.ToTable("TimesheetStatus");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.WorkPackage", b =>
                {
                    b.Property<string>("ProjectNumber");

                    b.Property<string>("WorkPackageNumber");

                    b.Property<int>("Budget");

                    b.Property<string>("Description");

                    b.Property<string>("ResponsibleEngineerNumber");

                    b.HasKey("ProjectNumber", "WorkPackageNumber");

                    b.HasIndex("ResponsibleEngineerNumber");

                    b.ToTable("WorkPackages");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.WPassignment", b =>
                {
                    b.Property<string>("ProjectNumber");

                    b.Property<string>("WorkPackageNumber");

                    b.Property<string>("EmployeeNumber");

                    b.HasKey("ProjectNumber", "WorkPackageNumber", "EmployeeNumber");

                    b.HasIndex("EmployeeNumber", "ProjectNumber");

                    b.ToTable("WPassignments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetApplication.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.ApplicationUser", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeNumber");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Employee", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.LaborGrade", "LaborGrade")
                        .WithMany("Employees")
                        .HasForeignKey("Grade");

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Employee", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorNumber");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Project", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.ProjectStatus", "Status")
                        .WithMany("Projects")
                        .HasForeignKey("StatusName");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.ProjectTeam", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Employee", "Employee")
                        .WithMany("ProjectTeams")
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Project", "Project")
                        .WithMany("ProjectTeams")
                        .HasForeignKey("ProjectNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.REBbyGrade", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.LaborGrade", "LaborGrade")
                        .WithMany("REBbyGrades")
                        .HasForeignKey("Grade")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.ResponsibleEngineerBudget", "ResponsibleEngineerBudget")
                        .WithMany("REBbyGrade")
                        .HasForeignKey("ProjectNumber", "WorkPackageNumber", "EndDate")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.ResponsibleEngineerBudget", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.WorkPackage", "WorkPackage")
                        .WithMany("ResponsibleEngineerBudgets")
                        .HasForeignKey("ProjectNumber", "WorkPackageNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.Timesheet", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Employee", "Employee")
                        .WithMany("Timesheets")
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.TimesheetStatus", "Status")
                        .WithMany("Timesheets")
                        .HasForeignKey("StatusName");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.TimesheetRow", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Timesheet", "Timesheet")
                        .WithMany("TimesheetRows")
                        .HasForeignKey("EmployeeNumber", "EndDate")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.WorkPackage", "WorkPackage")
                        .WithMany("TimesheetRows")
                        .HasForeignKey("ProjectNumber", "WorkPackageNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.WorkPackage", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Project", "Project")
                        .WithMany("WorkPackages")
                        .HasForeignKey("ProjectNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.Employee", "ResponsibleEngineer")
                        .WithMany("WorkPackages")
                        .HasForeignKey("ResponsibleEngineerNumber");
                });

            modelBuilder.Entity("TimeSheetApplication.Models.TimeSheetSystem.WPassignment", b =>
                {
                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.ProjectTeam", "ProjectTeam")
                        .WithMany("WPassignment")
                        .HasForeignKey("EmployeeNumber", "ProjectNumber");

                    b.HasOne("TimeSheetApplication.Models.TimeSheetSystem.WorkPackage", "WorkPackage")
                        .WithMany("WPassignment")
                        .HasForeignKey("ProjectNumber", "WorkPackageNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
