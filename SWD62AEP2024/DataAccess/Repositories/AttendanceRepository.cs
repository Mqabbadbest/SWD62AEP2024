using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class AttendanceRepository
    {
        private AttendanceContext _attendanceContext;
        public AttendanceRepository(AttendanceContext attendanceContext) {
            _attendanceContext = attendanceContext;
        }

        public void AddAttendance(Attendance a)
        {
            a.Timestamp = DateTime.Now;
            _attendanceContext.Attendances.Add(a);
            _attendanceContext.SaveChanges();
        }

        public IQueryable<Attendance> GetAttendances(DateTime date, string groupCode, string subjectCode)
        {
            return _attendanceContext.Attendances.Where(a => a.Timestamp.Date == date.Date && a.SubjectFK == subjectCode && a.Student.GroupFK == groupCode);
        }

        public IQueryable<Attendance> GetAttendances()
        {
            return _attendanceContext.Attendances;
        }
    }
}
