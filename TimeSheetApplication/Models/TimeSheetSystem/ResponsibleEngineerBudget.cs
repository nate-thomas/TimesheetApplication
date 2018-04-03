using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public ICollection<REBbyGrade> REBbyGrade { get; set; }
    }
}
