using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using System.ComponentModel.DataAnnotations;
using StudentPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using Microsoft.AspNetCore.Authorization;


namespace StudentPortal.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if (viewModel != null)
            {
                var student = new Student
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Subscribed = viewModel.Subscribed,

                };
                await dbContext.Students.AddAsync(student);
                await dbContext.SaveChangesAsync();
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);   
          
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Student ViewModel)
        {
            var student = await dbContext.Students.FindAsync(ViewModel.Id);
            if (student is not null)
            {
                student.Name = ViewModel.Name;
                student.Email = ViewModel.Email;    
                student.Phone = ViewModel.Phone;    
                student.Subscribed = ViewModel.Subscribed;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Student ViewModel)
        {
  
            var student = await dbContext.Students.FindAsync(ViewModel.Id);
            if (student is not null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}
