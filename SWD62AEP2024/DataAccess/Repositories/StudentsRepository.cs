using DataAccess.DataContext;
using Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Repositories
{
    public class StudentsRepository
    {
        private AttendanceContext myContext;

        //Constructor Injection
        public StudentsRepository(AttendanceContext _myContext) {
            myContext = _myContext;
        }

        /// <summary>
        /// This method will return the entire list of students in the database.
        /// </summary>
        /// <returns>All students in database</returns>
        public IQueryable<Student> GetStudents()
        {
            return myContext.Students;
        }

        public Student GetStudent(int idCard)
        {
            return myContext.Students.SingleOrDefault(s => s.IdCard == idCard);
        }

        public void UpdateStudent(Student student)
        {

        }

        public void DeleteStudent(Student student)
        { }

        public void AddStudent(Student student)
        {
            myContext.Students.Add(student);
            myContext.SaveChanges();
        }
    }
}
