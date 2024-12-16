using DataAccess.DataContext;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class LogsDbRepository : ILogsRepository
    {

        private AttendanceContext myContext;

        public LogsDbRepository(AttendanceContext _myContext) {
            myContext = _myContext;
        }

        public void AddLog(Log myLog)
        {
            myContext.Logs.Add(myLog);
            myContext.SaveChanges();
        }

        public IQueryable<Log> LoadLogs()
        {
            return myContext.Logs;
        }
    }
}
