using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } 
         
        public int Credits { get; set; }
        public string? CourseCode {  get; set; }
    }
}
