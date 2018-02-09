using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class TimesheetRows
    {
        [Key]
        public string EmployeeNumer { get; set; }
        [Key]
        public DateTime EndDate { get; set; }

        public Timesheets Timesheet { get; set; }
    }
}
