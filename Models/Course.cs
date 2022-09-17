using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_MM.Models
{
    public class Course
    {
        public Course() { }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }

        // Navigation property on a collection
        public ICollection<Student> Students { get; set; }
    }
}
