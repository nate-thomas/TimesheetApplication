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

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employees item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetByEmployeeNumber", new { empNumber = item.EmployeeNumber }, item);
        }

        [HttpPut("{empNumber}")]
        public IActionResult Update(long empNumber, [FromBody] Employees item)
        {
            string empNumberStr = empNumber.ToString();
            if (!ModelState.IsValid || !String.Equals(item.EmployeeNumber, empNumberStr))
            {
                return BadRequest(ModelState);
            }

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            if (employee == null)
            {
                return NotFound();
            }

            //Set First Name if asked to
            if(item.FirstName != null)
            {
                employee.FirstName = item.FirstName;
            }
            //Set Last Name if asked to
            if (item.LastName != null)
            {
                employee.LastName = item.LastName;
            }
            //Set LaborGrade if asked to
            if (item.LaborGrade != null)
            {
                employee.LaborGrade = item.LaborGrade;
            }
            //Set Grade if asked to
            if (item.Grade != null)
            {
                employee.Grade = item.Grade;
            }
            //Set EmployeeIntials if asked to
            if (item.EmployeeIntials != null)
            {
                employee.EmployeeIntials = item.EmployeeIntials;
            }
            //Set Supervisor if asked to
            if (item.Supervisor != null)
            {
                employee.Supervisor = item.Supervisor;
            }
            //Set SupervisorNumber if asked to
            if (item.SupervisorNumber != null)
            {
                employee.SupervisorNumber = item.SupervisorNumber;
            }
            //Set AuthCode if asked to
            if (item.AuthCode != null)
            {
                employee.AuthCode = item.AuthCode;
            }
            //Set AuthorizationCode if asked to
            if (item.AuthorizationCode != null)
            {
                employee.AuthorizationCode = item.AuthorizationCode;
            }
            //Set Timesheets if asked to
            if (item.Timesheets != null)
            {
                employee.Timesheets = item.Timesheets;
            }
            //Check if new model doesn't break any FK Constraints
            try
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return new NoContentResult();
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpDelete("{empNumber}")]
        public IActionResult Delete(long empNumber)
        {
            string empNumberStr = empNumber.ToString();

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}