using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetApplication.Models.TimeSheetSystem {
    public class REBbyGrade {

        [Required]
        public string ProjectNumber { get; set; }
        [Required]
        public string WorkPackageNumber { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Grade { get; set; }

        public int EstimatedManHours { get; set; }


        //[ForeignKey("Grade")]
        public LaborGrade LaborGrade { get; set; }

        public ResponsibleEngineerBudget ResponsibleEngineerBudget { get; set; }
    }
}
