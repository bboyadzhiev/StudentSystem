using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Exercises")]
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        public string ExerciseName { get; set; }

        [Required]
        public string ExerciseContent { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual Course Course { get; set; }

        public Exercise()
        {
            this.Students = new HashSet<Student>();
        }
    }
}