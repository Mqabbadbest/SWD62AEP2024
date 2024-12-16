using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
    public class LogsFileRepository : ILogsRepository
    {
        private string _filename;

        public LogsFileRepository(IConfiguration configuration)
        {
            _filename = configuration["LogsFileName"];
            if (string.IsNullOrEmpty(_filename))
            {
                throw new ArgumentException("LogsFileName is not defined in the configuration file");
            }
        }

        public void AddLog(Log myLog)
        {
            //1. Load Logs from the file as a list
            var listOfExistentLogs = LoadLogs().ToList();
            //2. Add the new log to the list
            listOfExistentLogs.Add(myLog);
            //3. Save back the entire list to the file
            File.WriteAllText(_filename, JsonConvert.SerializeObject(listOfExistentLogs));
        }

        public IQueryable<Log> LoadLogs()
        {

            if(!File.Exists(_filename))
            {
                return new List<Log>().AsQueryable();
            }

            string contents = File.ReadAllText(_filename);

            var listOfLogs = JsonConvert.DeserializeObject<List<Log>>(contents);

            return listOfLogs.AsQueryable();
        }
    }
}
