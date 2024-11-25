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
            var subjects = _subjectsRepository.GetSubjects();
            var groups = _groupsRepository.GetGroups();

            List<SelectPastAttendanceViewModel> pastAttendances = _attendanceRepository.GetAttendances()
                .GroupBy(x => new
                {
                    Date = x.Timestamp,
                    SubjectCode = x.SubjectFK,
                    GroupCode = x.Student.GroupFK,
                    SubjectName = x.Subject.Name
                })
                .Select(g => new SelectPastAttendanceViewModel
                {
                    Date = g.Key.Date,
                    SubjectCode = g.Key.SubjectCode,
                    GroupCode = g.Key.GroupCode,
                    SubjectName = g.Key.SubjectName,
                })
                .ToList();

            SelectGroupSubjectViewModel viewModel = new SelectGroupSubjectViewModel()
            {
                Groups = groups.ToList(),
                Subjects = subjects.ToList(),
                PastAttendances = pastAttendances
            };

            return View(viewModel);
        }

        [HttpGet] //it needs to show me which students are supposed to be in that attendance list
        public IActionResult Create(string groupCode, string subjectCode, string attendanceButton)
        {

            if (attendanceButton == "0") {
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
                if (subject != null)
                {
                    viewModel.SubjectName = subject.Name;
                }
                else
                {
                    viewModel.SubjectName = "";
                }

                return View(viewModel);
            }
            else
            {
                string[] myValues= attendanceButton.Split(new char[] { '|' });
                DateTime date = Convert.ToDateTime(myValues[0]);
                string selectedSubjectCode = myValues[1];
                string selectedGroupCode = myValues[2];

                CreateAttendanceViewModel viewModel = new CreateAttendanceViewModel();
                viewModel.GroupCode = selectedGroupCode;
                viewModel.SubjectCode = selectedSubjectCode;
                viewModel.Students = _studentsRepository.GetStudents().Where(s => s.GroupFK == selectedGroupCode).OrderBy(s => s.FirstName).ToList();
                viewModel.Presence = _attendanceRepository.GetAttendances()
                    .Where(
                    s => s.SubjectFK == selectedSubjectCode 
                    && s.Timestamp.Day == date.Day 
                    && s.Timestamp.Month == date.Month 
                    && s.Timestamp.Year == date.Year 
                    && s.Timestamp.Minute == date.Minute)
                    .OrderBy(s => s.Student.FirstName)
                    .Select(s => s.IsPresent)
                    .ToList();

                return View(viewModel);
            }
        }

        [HttpPost] // it saves the absences and presences of all the studnets from the first Create method
        public IActionResult Create(List<Attendance> attendances)
        {

            if(attendances.Count > 0)
            {
                _attendanceRepository.AddAttendances(attendances);
            }

            return RedirectToAction("Index");
        }
    }
}
