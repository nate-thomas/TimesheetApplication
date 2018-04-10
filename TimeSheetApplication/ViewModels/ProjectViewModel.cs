using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.ViewModels
{
    public class ProjectViewModel
    {
        [Key]
        public string ProjectNumber { get; set; }

        public string StatusName { get; set; }

        public string Description { get; set; }

        public int Budget { get; set; }

        public string ProjectManager { get; set; }
    }
}
