using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Presentation.Models;

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

        //Handle the redirection from the list page to the actual edit page
        //This one is going to load the existant details for the end user to see in the textboxes
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentRepository.GetStudent(id);
            if (student == null)
            {
                //It will take the end user to the page we want to redirect them to
                return RedirectToAction("List");
            }
            else
            {
                return View(student);
            }
        }

        [HttpPost]
        //Handle the click of the Submit Changes button
        public IActionResult Update(Student student)
        {
            try
            {
                if (student == null)
                {
                    //Session - keeps the data in scope on the server side (across Controllers etc.)
                    // ViewData - keeps the data in scope between Controller and View for 1 response only NOT a redirection
                    // TempData - This is like ViewData but the data is kept in scope for 1 redirection only.
                    // ViewBag - Exactly like the ViewData but it allows us to create variables on the fly (ex: ViewBag.MyVar = "Hello"; instead of ViewData["MyVar"] = "Hello";)
                    TempData["Error"] = "Student not found";

                    return Redirect("Error");
                }
                else
                {
                    //Validations, sanitizations of data

                    ModelState.Remove(nameof(Student.Group));

                    //This line will ensure that if there are validation policies (Centralized or not)
                    //applied, they will have to pass from here; it ensures that validations have been triggered
                    if (ModelState.IsValid)
                    {
                        _studentRepository.UpdateStudent(student);
                        TempData["Message"] = "Student updated successfully!";
                        return RedirectToAction("List");
                    }

                    TempData["Error"] = "Check your inputs.";
                    return View("Edit", student);
                }
            }
            catch
            {
                TempData["Error"] = "Something went wrong. We are sorry.";
                return Redirect("Error");
            }
        }


        [HttpGet] //Used to load the page with empty textboxes
        public IActionResult Create([FromServices] GroupsRepository groupRepo)
        {

            //Eventually: We need to fetch the list of existing groups
            var myGroups = groupRepo.GetGroups();

            // How are we going to pass the myGroups into the View?
            //Approach 1 - we can pass a model into the view where we create a ViewModel
            // Problem is, you cannot pass IQueryable<Group> model into the Student model
            StudentCreateViewModel myModel = new StudentCreateViewModel();
            myModel.Groups = myGroups.ToList();

            return View(myModel);
            //Approach 2
        }
         
        [HttpPost] //Is triggered by the submit of the form
        public IActionResult Create(Student student, [FromServices] GroupsRepository groupRepo)
        {
            if (_studentRepository.GetStudent(student.IdCard) != null)
            {
                TempData["Error"] = "Student already exists.";
                return RedirectToAction("List");
            }
            else
            {
                ModelState.Remove(nameof(Student.Group));

                //This line will ensure that if there are validation policies (Centralized or not)
                //applied, they will have to pass from here; it ensures that validations have been triggered
                if (ModelState.IsValid)
                {
                    _studentRepository.AddStudent(student);
                    TempData["Message"] = "Student was added successfully!";
                    return RedirectToAction("List");
                }

                TempData["Error"] = "Check your inputs.";

                //Populating a StudentCreateViewModel
                var myGroups = groupRepo.GetGroups();
                StudentCreateViewModel myModel = new StudentCreateViewModel();
                myModel.Groups = myGroups.ToList();
                //Why do I assign the student that was submitted int his method?
                //Passing the same instance back to the page
                //so that I show the end-user the same data they submitted
                myModel.Student = student;

                return View(myModel); //Will be looking for a view as the action...Create
            }
        }
    }
}
