using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class LogsRepository
    {

        private AttendanceContext myContext;

        public LogsRepository(AttendanceContext _myContext) {
            myContext = _myContext;
        }

        public void AddLog(Log myLog)
        {
            myContext.Logs.Add(myLog);
            myContext.SaveChanges();
        }
    }
}
