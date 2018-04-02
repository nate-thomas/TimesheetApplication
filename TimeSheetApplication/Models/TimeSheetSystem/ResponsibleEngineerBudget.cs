using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class ResponsibleEngineerBudget
    {
        [Required]
        public string ProjectNumber { get; set; }
        [Required]
        public string WorkPackageNumber { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public int EstimatedBudget { get; set; }
        public int ActualBudget { get; set; }


        public WorkPackage WorkPackage { get; set; }
    }
}
