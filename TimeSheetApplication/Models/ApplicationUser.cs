using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeNumber { get; set; }

        [ForeignKey("EmployeeNumber")]
        public Employee Employee { get; set; }
    }
}
