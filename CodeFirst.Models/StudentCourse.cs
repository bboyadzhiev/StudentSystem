using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class StudentCourse
    {
        [Column(Order = 0), Key, ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Column(Order = 1), Key, ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int Result { get; set; }
    }
}
