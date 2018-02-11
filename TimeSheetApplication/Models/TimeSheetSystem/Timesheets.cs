using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Timesheets
    {

        [Required]
        public string EmployeeNumber { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey("EmployeeNumber")]
        public Employees Employee { get; set; }
        //public List<TimesheetRows> TimesheetRows { get; set; }
    }
}
