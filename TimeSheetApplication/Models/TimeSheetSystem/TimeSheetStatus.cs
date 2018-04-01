using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class TimesheetStatus
    {
        [Key]
        public string StatusName { get; set; }
        public string Description { get; set; }


        public ICollection<Timesheet> Timesheets { get; set; }
    }
}
