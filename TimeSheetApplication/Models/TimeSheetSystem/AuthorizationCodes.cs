using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetApplication.Models.TimeSheetSystem
{
    public class AuthorizationCodes
    {
        [Key]
        [Required]
        public string AuthCode { get; set; }

        [ForeignKey("AuthCode")]
        public ICollection<Employees> Employees { get; set; }
    }
}
