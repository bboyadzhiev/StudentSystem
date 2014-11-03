using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("People")]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [Index("IX_SNN", IsUnique = true)]
        [MinLength(10)]
        [MaxLength(10)]
        public string SSN { get; set; }
    }
}