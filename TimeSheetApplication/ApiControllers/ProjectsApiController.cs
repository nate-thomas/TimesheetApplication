using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Data;
using TimeSheetApplication.Models.TimeSheetSystem;
using TimeSheetApplication.ViewModels;

namespace TimeSheetApplication.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        // GET: api/ProjectsApi/5
        [HttpGet("{ProjectNumber}")]
        public async Task<IActionResult> GetProject([FromRoute] string ProjectNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectNumber == ProjectNumber);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet("pm/{pmNumber}")]
        public async Task<IActionResult> GetProjectsByPM([FromRoute] string pmNumber)
        {
            List<ProjectViewModel> projectsList = new List<ProjectViewModel>();
            var projects = _context.Projects.ToArray<Project>();
            foreach (Project p in projects)
            {
                if(p.ProjectManager != null && p.ProjectManager.Equals(pmNumber))
                {
                    ProjectViewModel temp = new ProjectViewModel
                    {
                        ProjectNumber = p.ProjectNumber,
                        StatusName = p.StatusName,
                        Description = p.Description,
                        Budget = p.Budget,
                        ProjectManager = p.ProjectManager
                    };
                    projectsList.Add(temp);
                }
            }
            if (projectsList.Count == 0)
            {
                return BadRequest("Prokect Manager not found");
            }
            return new ObjectResult(projectsList);
        }

        // PUT: api/ProjectsApi/5
        [HttpPut("{ProjectNumber}")]
        public async Task<IActionResult> PutProject([FromRoute] string ProjectNumber, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ProjectNumber != project.ProjectNumber)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(ProjectNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjectsApi
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.ProjectNumber }, project);
        }

        // DELETE: api/ProjectsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectNumber == id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        private bool ProjectExists(string id)
        {
            return _context.Projects.Any(e => e.ProjectNumber == id);
        }
    }
}