using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Project
    {
        [Key]
        public string ProjectNumber { get; set; }

        public string StatusName { get; set; }

        public string Description { get; set; }

        public int Budget { get; set; }

        public string ProjectManager { get; set; }

        [ForeignKey("ProjectManager")]
        public Employee PM { get; set; }

        public ProjectStatus Status { get; set; }

        public ICollection<WorkPackage> WorkPackages { get; set; }

        public ICollection<ProjectTeam> ProjectTeams { get; set; }
    }
}
