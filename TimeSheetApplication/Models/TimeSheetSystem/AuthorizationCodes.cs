using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class AuthorizationCodes
    {
        [Key]
        public string AuthCode { get; set; }

        public List<Employees> Employees { get; set; }
    }
}
