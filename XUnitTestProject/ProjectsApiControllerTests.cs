using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TimeSheetApplication.ApiControllers;
using TimeSheetApplication.Interfaces;
using Xunit;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Models.TimeSheetSystem;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTestProject
{
    public class ProjectsApiControllerTests
    {
        [Fact]
        public void GetAllProjects()
        {
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testProjects);
            dbContext.Setup(c => c.Projects).Returns(mockList.Object);

            var controller = new ProjectsApiController(dbContext.Object);
            var resultList = controller.GetProjects().ToList();

            Assert.Equal(5, resultList.Count);
        }

        [Fact]
        public void GetProject_WhenModelStateIsInvalid()
        {
            string projectNumber = "abc123";
            var controller = new ProjectsApiController(null);
            controller.ModelState.AddModelError("key", "message");
            var result = controller.GetProject(projectNumber);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact(Skip = "Issue with SingleOrDefaultAsync call")]
        public void GetProject_WhenProjectNotFound()
        {
            string projectNumber = "x";
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testProjects);
            dbContext.Setup(c => c.Projects).Returns(mockList.Object);

            var controller = new ProjectsApiController(dbContext.Object);

            var result = controller.GetProject(projectNumber);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PutProject_WhenModelStateIsInvalid()
        {
            var controller = new ProjectsApiController(null);
            controller.ModelState.AddModelError("key", "message");
            var result = controller.PutProject(null, null);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void PutProject_WhenIdDoesNotMatchProjectNumber()
        {
            string id = "abc123";
            var project = testProjects[0];
            var controller = new ProjectsApiController(null);
            int code = 400;

            var result = controller.PutProject(id, project);

            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(code, badRequestResult.StatusCode);
        }

        [Fact(Skip = "Incomplete: mock dbcontext.Entry()")]
        public void PutProject_()
        {
            string id = "WebPrj128";
            var project = testProjects[0];

            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testProjects);
            dbContext.Setup(c => c.Projects).Returns(mockList.Object);
            var controller = new ProjectsApiController(dbContext.Object);

            var result = controller.PutProject(id, project);
        }

        [Fact]
        public void PostProject_WhenModelStateIsInvalid()
        {
            Project project = testProjects[0];
            var controller = new ProjectsApiController(null);
            controller.ModelState.AddModelError("key", "message");

            var result = controller.PostProject(project);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void PostProject_Successful()
        {
            Project project = testProjects[0];
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testProjects);
            dbContext.Setup(x => x.Projects).Returns(mockList.Object);
            var controller = new ProjectsApiController(dbContext.Object);

            var result = controller.PostProject(project);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        /* Helper methods and sample data */
        List<Project> testProjects = new List<Project>()
        {
            new Project {ProjectNumber = "WebPrj128", Description = "[Current] Web project for BC Hydro (Prj02)", StatusName="Current", Budget = 15000},
            new Project {ProjectNumber = "WebPrj098", Description = "[Current] Web project for Burnaby Publc Library", StatusName="Current", Budget = 13000},
            new Project {ProjectNumber = "WebPrj2018", Description = "[Archieved] Web project for Oranj Fitness", StatusName="Archived", Budget = 50000},
            new Project {ProjectNumber = "WebPrj127", Description = "[Current] Web project for BC Hydro (Prj01)", StatusName="Current", Budget = 75000},
            new Project {ProjectNumber = "SW789", Description = "[Current] Software project for Lululemon", StatusName="Current", Budget = 5000}
        };

        Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSetMock;
        }
    }
}
