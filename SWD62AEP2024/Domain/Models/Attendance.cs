using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        //Foreign key
        public int StudentFK { get; set; }
        //Navigational property
        [ForeignKey("StudentFK")]
        public Student Student { get; set; }

        public bool IsPresent { get; set; }

        public DateTime Timestamp { get; set; }

        public string SubjectFK { get; set; }

        [ForeignKey("SubjectFK")]
        public Subject Subject { get; set; }
    }
}
