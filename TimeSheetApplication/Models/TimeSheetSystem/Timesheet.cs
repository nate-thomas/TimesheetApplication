using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Timesheet
    {

        [Required]
        public string EmployeeNumber { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey("EmployeeNumber")]
        public Employee Employee { get; set; }

        [ForeignKey("StatusId")]
        public TimesheetStatus Status { get; set; }

        public ICollection<TimesheetRow> TimesheetRows { get; set; }
    }
}
