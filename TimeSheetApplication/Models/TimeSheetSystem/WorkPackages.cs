using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class WorkPackages
    {
        [Key]
        public string ProjectNumber { get; set; }
        [Key]
        public string WorkPackageNumber { get; set; }
        public string Description { get; set; }


        public Projects Project { get; set; }
    }
}
