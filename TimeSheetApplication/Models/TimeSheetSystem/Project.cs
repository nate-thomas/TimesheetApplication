using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Project
    {
        [Key]
        public string ProjectNumber { get; set; }

        public string Description { get; set; }
        
        public ICollection<WorkPackage> WorkPackages { get; set; }
    }
}
