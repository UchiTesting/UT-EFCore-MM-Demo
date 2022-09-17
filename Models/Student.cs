using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_MM.Models
{
    public class Student
    {
        public Student() { }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        // Navigation property on a collection
        IEnumerable<Course> Courses { get; set; }
    }
}
