using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    public enum Title
    {
        PhD, Assistant, Proffessor
    }

    [Table("Lecturers")]
    public class Lecturer : Person
    {
        [Required]
        public Title Title { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public Lecturer()
        {
            this.Courses = new HashSet<Course>();
        }
    }
}