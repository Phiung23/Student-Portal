using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this namespace
using Microsoft.EntityFrameworkCore;
using StudentPortal.Models.Entities;

namespace StudentPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Inherit from IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Your existing DbSets for application entities
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        // You can add more DbSets for other entities if needed
    }
}
