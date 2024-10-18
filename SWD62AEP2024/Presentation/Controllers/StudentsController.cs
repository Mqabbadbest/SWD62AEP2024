using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    //As a good a practice of how we are structuring our architecture:

    //Keep the repository classes to interact directly with the database.
    //Keep the controllers to handle requests/responses i.e. user input and then sanitize accordingly
    //in other words do not make any calls directly to the database in the controller.

    public class StudentsController : Controller
    {
        private StudentsRepository _studentRepository;

        //Contructor Injection
        public StudentsController(StudentsRepository studentsRepository)
        {
            _studentRepository = studentsRepository;
        }

        //Method Injection - List([FromServices] StudentsRepository myRepo)
        public IActionResult List()
        {
            var list = _studentRepository.GetStudents();
            return View(list); //We are passing into the View, the list contaning the fetched students
        }
    }
}
