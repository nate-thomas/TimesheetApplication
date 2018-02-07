using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Employees
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public LaborGrades LaborGrade { get; set; }
        public string Grade { get; set; }

        public string EmployeeIntials { get; set; }
        public Employees Supervisor { get; set; }

        public string SupervisorNumber { get; set; }
        //TODO: Determine if there should be a column to indicate if the individual can be a supervisor
        //public AuthorizationCodes AuthCode { get; set; }
    }
}
