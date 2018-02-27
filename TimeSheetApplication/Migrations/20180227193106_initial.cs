using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TimeSheetApplication.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorizationCodes",
                columns: table => new
                {
                    AuthCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationCodes", x => x.AuthCode);
                });

            migrationBuilder.CreateTable(
                name: "LaborGrades",
                columns: table => new
                {
                    Grade = table.Column<string>(nullable: false),
                    PayAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborGrades", x => x.Grade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectNumber = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectNumber);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeNumber = table.Column<string>(nullable: false),
                    AuthCode = table.Column<string>(nullable: true),
                    EmployeeIntials = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SupervisorEmployeeNumber = table.Column<string>(nullable: true),
                    SupervisorNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeNumber);
                    table.ForeignKey(
                        name: "FK_Employees_AuthorizationCodes_AuthCode",
                        column: x => x.AuthCode,
                        principalTable: "AuthorizationCodes",
                        principalColumn: "AuthCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_LaborGrades_Grade",
                        column: x => x.Grade,
                        principalTable: "LaborGrades",
                        principalColumn: "Grade",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_SupervisorEmployeeNumber",
                        column: x => x.SupervisorEmployeeNumber,
                        principalTable: "Employees",
                        principalColumn: "EmployeeNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkPackages",
                columns: table => new
                {
                    ProjectNumber = table.Column<string>(nullable: false),
                    WorkPackageNumber = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPackages", x => new { x.ProjectNumber, x.WorkPackageNumber });
                    table.ForeignKey(
                        name: "FK_WorkPackages_Projects_ProjectNumber",
                        column: x => x.ProjectNumber,
                        principalTable: "Projects",
                        principalColumn: "ProjectNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    EmployeeNumber = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => new { x.EmployeeNumber, x.EndDate });
                    table.ForeignKey(
                        name: "FK_Timesheets_Employees_EmployeeNumber",
                        column: x => x.EmployeeNumber,
                        principalTable: "Employees",
                        principalColumn: "EmployeeNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetRows",
                columns: table => new
                {
                    EmployeeNumber = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ProjectNumber = table.Column<string>(nullable: false),
                    WorkPackageNumber = table.Column<string>(nullable: false),
                    Friday = table.Column<double>(nullable: false),
                    Monday = table.Column<double>(nullable: false),
                    Saturday = table.Column<double>(nullable: false),
                    Sunday = table.Column<double>(nullable: false),
                    Thursday = table.Column<double>(nullable: false),
                    TimesheetRowsId = table.Column<string>(nullable: true),
                    Tuesday = table.Column<double>(nullable: false),
                    Wednesday = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetRows", x => new { x.EmployeeNumber, x.EndDate, x.ProjectNumber, x.WorkPackageNumber });
                    table.ForeignKey(
                        name: "FK_TimesheetRows_Timesheets_EmployeeNumber_EndDate",
                        columns: x => new { x.EmployeeNumber, x.EndDate },
                        principalTable: "Timesheets",
                        principalColumns: new[] { "EmployeeNumber", "EndDate" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetRows_WorkPackages_ProjectNumber_WorkPackageNumber",
                        columns: x => new { x.ProjectNumber, x.WorkPackageNumber },
                        principalTable: "WorkPackages",
                        principalColumns: new[] { "ProjectNumber", "WorkPackageNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AuthCode",
                table: "Employees",
                column: "AuthCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Grade",
                table: "Employees",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorEmployeeNumber",
                table: "Employees",
                column: "SupervisorEmployeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetRows_ProjectNumber_WorkPackageNumber",
                table: "TimesheetRows",
                columns: new[] { "ProjectNumber", "WorkPackageNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimesheetRows");

            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "WorkPackages");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "AuthorizationCodes");

            migrationBuilder.DropTable(
                name: "LaborGrades");
        }
    }
}
