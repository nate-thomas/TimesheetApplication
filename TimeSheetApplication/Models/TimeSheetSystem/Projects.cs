using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Projects
    {
        [Key]
        public string ProjectNumber { get; set; }
        public string Description { get; set; }


        public ICollection<WorkPackages> WorkPackages { get; set; }
    }
}
