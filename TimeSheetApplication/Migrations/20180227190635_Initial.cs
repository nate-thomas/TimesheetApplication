using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TimeSheetApplication.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetRows_Employees_EmployeeNumber",
                table: "TimesheetRows");

            migrationBuilder.AddColumn<string>(
                name: "TimesheetRowsId",
                table: "TimesheetRows",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesheetRowsId",
                table: "TimesheetRows");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetRows_Employees_EmployeeNumber",
                table: "TimesheetRows",
                column: "EmployeeNumber",
                principalTable: "Employees",
                principalColumn: "EmployeeNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
