using DataAccess.DataContext;
using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class AttendanceController : Controller
    {
        private AttendanceRepository _attendanceRepository;
        public AttendanceController(AttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        //A page which shows which attendances I can take
        public IActionResult Index()
        {
            //Note: getting all attendances, but then extracting soe information which I will put on screen
            //      being Subject, Date and Group
            //...this requires me to create a ViewModel to accomodate this newly group of data 

            var groupedAttendances = _attendanceRepository
                .GetAttendances()
                    .GroupBy(a => new { a.SubjectFK, a.Student.GroupFK, a.Timestamp.Date, Subject = a.Subject.Name })
                    .OrderByDescending(x => x.Key.Date)
                    .Select(g => new AttendancesListViewModel()
                    {
                        SubjectFK = g.Key.SubjectFK,
                        Group = g.Key.GroupFK,
                        SubjectName = g.Key.Subject,
                        Date = g.Key.Date,
                    })
                    .ToList();
            return View(groupedAttendances);
        }

        [HttpGet] //it needs to show me which students are supposed to be in that attendance list
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // it saves the absences and presences of all the studnets from the first Create method
        public IActionResult Create()
        {
            return View();
        }
    }
}
