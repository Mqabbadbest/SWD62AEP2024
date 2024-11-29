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

        public void AddAttendances(List<Attendance> attendances)
        {
            var currentTime = DateTime.Now;

            foreach(var a in attendances)
            {
                a.Timestamp = currentTime; //meaning all the records are going to get the same exact time including the milliseconds
                _attendanceContext.Attendances.Add(a);
            }

            _attendanceContext.SaveChanges(); //Call this once at the end. This will refrain from opening a connection to the database multiple times.
        }

        public IQueryable<Attendance> GetAttendances(DateTime date, string groupCode, string subjectCode)
        {
            return _attendanceContext.Attendances.Where(a => a.Timestamp.Date == date.Date && a.SubjectFK == subjectCode && a.Student.GroupFK == groupCode);
        }

        public IQueryable<Attendance> GetAttendances()
        {
            return _attendanceContext.Attendances;
        }

        public void UpdateAttendances(List<Attendance> attendances)
        {
            foreach(var a in attendances)
            {
                var oldRecord = GetAttendances()
                                .SingleOrDefault(x => x.StudentFK == a.Id);
                oldRecord.IsPresent = a.IsPresent;
            }
            _attendanceContext.SaveChanges();
        }
    }
}
