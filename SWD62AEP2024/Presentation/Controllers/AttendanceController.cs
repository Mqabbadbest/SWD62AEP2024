using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class AttendanceController : Controller
    {
        private AttendanceRepository _attendanceRepository;
        private GroupsRepository _groupsRepository;
        private SubjectsRepository _subjectsRepository;
        private StudentsRepository _studentsRepository;

        public AttendanceController(
            AttendanceRepository attendanceRepository,
            GroupsRepository groupsRepository,
            SubjectsRepository subjectsRepository,
            StudentsRepository studentsRepository
            )
        {
            _attendanceRepository = attendanceRepository;
            _groupsRepository = groupsRepository;
            _subjectsRepository = subjectsRepository;
            _studentsRepository = studentsRepository;
        }

        //A page which shows which attendances I can take
        public IActionResult Index()
        {
            //var groupedAttendances = _attendanceRepository
            //    .GetAttendances()
            //        .GroupBy(a => new { a.SubjectFK, a.Student.GroupFK, a.Timestamp.Date, Subject = a.Subject.Name })
            //        .OrderByDescending(x => x.Key.Date)
            //        .Select(g => new AttendancesListViewModel()
            //        {
            //            SubjectFK = g.Key.SubjectFK,
            //            Group = g.Key.GroupFK,
            //            SubjectName = g.Key.Subject,
            //            Date = g.Key.Date,
            //        })
            //        .ToList();
            //return View(groupedAttendances);

            var subjects = _subjectsRepository.GetSubjects();
            var groups = _groupsRepository.GetGroups();

            SelectGroupSubjectViewModel viewModel = new SelectGroupSubjectViewModel()
            {
                Groups = groups.ToList(),
                Subjects = subjects.ToList()
            };

            return View(viewModel);
        }

        [HttpGet] //it needs to show me which students are supposed to be in that attendance list
        public IActionResult Create(string groupCode, string subjectCode)
        {

            var students = _studentsRepository.GetStudents() //Select * from Students
                .Where(s => s.GroupFK == groupCode)// Select * from Students where GroupFK = groupCode
                .OrderBy(s => s.FirstName) // Order by FirstName
                .ToList(); // Execute the query

            CreateAttendanceViewModel viewModel = new CreateAttendanceViewModel()
            {
                GroupCode = groupCode,
                SubjectCode = subjectCode,
                Students = students
            };

            Subject subject = _subjectsRepository.GetSubjects().SingleOrDefault(s => s.Code == subjectCode);
            if(subject != null)
            {
                viewModel.SubjectName = subject.Name;
            }
            else
            {
                viewModel.SubjectName = "";
            }

            return View(viewModel);
        }

        [HttpPost] // it saves the absences and presences of all the studnets from the first Create method
        public IActionResult Create()
        {
            return View();
        }
    }
}
