using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class WPassignment
    {
        [Required]
        public string EmployeeNumber { get; set; }
        [Required]
        public string ProjectNumber { get; set; }
        [Required]
        public string WorkPackageNumber { get; set; }

        public ProjectTeam ProjectTeam { get; set; }
        public WorkPackage WorkPackage { get; set; }
    }
}
