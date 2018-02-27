using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheetApplication.Data;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/EmployeesAPI")]
    public class EmployeesAPIController : Controller
    {
        private readonly ApplicationDbContext _context;


        public EmployeesAPIController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<Employees> GetAll()
        {
            return _context.Employees.ToList();
        }

        [HttpGet("{empNumber}", Name = "GetByEmployeeNumber")]
        public IActionResult GetByEmployeeNumber(long empNumber)
        {
            string empNumberStr = empNumber.ToString();

            var item = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}