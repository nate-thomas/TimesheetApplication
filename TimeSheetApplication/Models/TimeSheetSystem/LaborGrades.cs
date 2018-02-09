using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class LaborGrades
    {
        [Key]
        public string Grade { get; set; }
        public double PayAmount { get; set; }

        public List<Employees> Employees { get; set; }
    }
}
