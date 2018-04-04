using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Employee
    {
        [Key]
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Grade { get; set; }
        public string EmployeeIntials { get; set; }
        public string SupervisorNumber { get; set; }

        [ForeignKey("Grade")]
        public LaborGrade LaborGrade { get; set; }
        [ForeignKey("SupervisorNumber")]
        public Employee Supervisor { get; set; }
        //TODO: Determine if there should be a column to indicate if the individual can be a supervisor
        public ICollection<Timesheet> Timesheets { get; set; }
        public ICollection<ProjectTeam> ProjectTeams { get; set; }
    }
}
