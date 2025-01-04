using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entities;

namespace StudentPortal.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public CoursesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseViewModel NewCourse)
        {
            if (NewCourse != null)
            {
                var course = new Course
                {
                    Name = NewCourse.Name,
                    CourseCode = NewCourse.CourseCode,
                    Description = NewCourse.Description,
                    Credits = int.Parse(NewCourse.Credits)
                };
                await dbContext.Courses.AddAsync(course);
                await dbContext.SaveChangesAsync();
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var courses = await dbContext.Courses.ToListAsync();
            return View(courses);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid CourseID)
        {
            var course = await dbContext.Courses.FindAsync(CourseID);
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Course changeCourse)
        {
            if (changeCourse != null)
            {
                var oldCourse = await dbContext.Courses.FindAsync(changeCourse.CourseId);
                if (oldCourse != null)
                {
                    oldCourse.Name = changeCourse.Name;
                    oldCourse.Description = changeCourse.Description;   
                    oldCourse.Credits = changeCourse.Credits;
                    oldCourse.CourseCode = changeCourse.CourseCode;
                    await dbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("List", "Courses");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Course deleteCourse)
        {
            var course = await dbContext.Courses.FindAsync(deleteCourse.CourseId);
            if (course != null) 
            { 
                dbContext.Courses.Remove(course);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Courses");
        }
    }
}
