using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public LaborGrades LaborGrade { get; set; }
        public string Grade { get; set; }

        public string EmployeeIntials { get; set; }
        public Employees Supervisor { get; set; }

        public string SupervisorNumber { get; set; }
        //TODO: Determine if there should be a column to indicate if the individual can be a supervisor
        
        public string AuthCode { get; set; }
        public AuthorizationCodes AuthorizationCode { get; set; }
    }
}
