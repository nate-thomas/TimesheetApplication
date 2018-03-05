using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Employees
    {   
        [Key]
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AppUser Identity { get; set; } //will this fix the error?

        [ForeignKey("Grade")]
        public LaborGrades LaborGrade { get; set; }
        public string Grade { get; set; }

        public string EmployeeIntials { get; set; }
        public Employees Supervisor { get; set; }

        public string SupervisorNumber { get; set; }
        //TODO: Determine if there should be a column to indicate if the individual can be a supervisor
        
        public string AuthCode { get; set; }

        [ForeignKey("AuthCode")]
        public AuthorizationCodes AuthorizationCode { get; set; }

        public ICollection<Timesheets> Timesheets { get; set; }
    }
}
