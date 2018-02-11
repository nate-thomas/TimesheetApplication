using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class TimesheetRows
    {
        public String TimesheetRowsId { get; set; }
        
        [Required]
        public string EmployeeNumber { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string ProjectNumber { get; set; }
        [Required]
        public string WorkPackageNumber { get; set; }

        public double Saturday { get; set; }
        public double Sunday { get; set; }
        public double Monday { get; set; }
        public double Tuesday { get; set; }
        public double Wednesday { get; set; }
        public double Thursday { get; set; }
        public double Friday { get; set; }

        public WorkPackages WorkPackage { get; set; }
        public Timesheets Timesheet { get; set; }


    }
}
