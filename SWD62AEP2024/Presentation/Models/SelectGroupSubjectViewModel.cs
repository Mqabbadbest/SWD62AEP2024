using Domain.Models;

namespace Presentation.Models
{
    public class SelectGroupSubjectViewModel
    {
        public List<Group> Groups { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<SelectPastAttendanceViewModel> PastAttendances { get; set; }
    }

    public class SelectPastAttendanceViewModel
    {
        public string GroupCode { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public DateTime Date { get; set; }
    }
}
