namespace CodeFirst.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Homeworks")]
    public class Homework
    {
        [Key]
        public int HomeworkID { get; set; }

        [Required]
        public string HomeworkContent { get; set; }

        public DateTime TimeSent { get; set; }

        [Required]
        public virtual Course Course { get; set; }

        [Required]
        public virtual Student Student { get; set; }
    }
}