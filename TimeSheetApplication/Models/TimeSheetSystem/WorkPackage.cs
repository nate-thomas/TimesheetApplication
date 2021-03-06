﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class WorkPackage
    {
        public string ProjectNumber { get; set; }
        public string WorkPackageNumber { get; set; }
        public string Description { get; set; }
        public int Budget { get; set; }
        public string ResponsibleEngineerNumber { get; set; }
        
        

        [ForeignKey("ProjectNumber")]
        public Project Project { get; set; }

        [ForeignKey("ResponsibleEngineerNumber")]
        public Employee ResponsibleEngineer { get; set; }


        public ICollection<TimesheetRow> TimesheetRows { get; set; }
        public ICollection<ResponsibleEngineerBudget> ResponsibleEngineerBudgets { get; set; }
        public ICollection<WPassignment> WPassignment { get; set; }
    }
}
