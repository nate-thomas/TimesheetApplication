﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Timesheets
    {
        public String TimesheetsId { get; set; }
        /*[Key]
        [Required]
        public string EmployeeNumber { get; set; }
        [Key]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }


        public Employees Employee { get; set; }
        public List<TimesheetRows> TimesheetRows { get; set; }*/
    }
}
