using Domain.Models;
namespace Presentation.Models
{
    public class CreateAttendanceViewModel
    {
        public CreateAttendanceViewModel()
        {
            Attendances = new List<Attendance>();
        }

        public List<Student> Students { get; set; }
        public List<Attendance> Attendances { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string GroupCode { get; set; }

    }
}
