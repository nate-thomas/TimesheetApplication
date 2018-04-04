using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class ProjectTeam
    {
        public string ProjectNumber { get; set; }

        public string EmployeeNumber { get; set; }

        [ForeignKey("ProjectNumber")]
        public Project Project { get; set; }

        [ForeignKey("EmployeeNumber")]
        public Employee Employee { get; set; }


        public ICollection<WPassignment> WPassignment { get; set; }
    }
}
