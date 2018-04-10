﻿using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApplication.Data;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.ApiControllers
{
    [Produces("application/json")]
    [Route("api/LaborGrades")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class LaborGradesController : Controller
    {
        private readonly IDbContext _context;

        public LaborGradesController(IDbContext context)
        {
            _context = context;
        }

        // GET: api/LaborGrades
        [HttpGet]
        public IEnumerable<LaborGrade> GetLaborGrades()
        {
            return _context.LaborGrades.ToList();
        }
        
    }
}