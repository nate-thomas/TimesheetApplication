using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class Timesheets
    {
        [Key]
        public string EmployeeNumer { get; set; }
        [Key]
        public DateTime EndDate { get; set; }
        [Key]
        public string ProjectNumber { get; set; }
        [Key]
        public string WorkPackageNumber { get; set; }


        public WorkPackages WorkPackage{ get; set; }
        public Employees Employee { get; set; }
    }
}
