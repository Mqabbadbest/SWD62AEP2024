using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class SubjectsRepository
    {
        private AttendanceContext myContext;

        public SubjectsRepository(AttendanceContext context)
        {
            myContext = context;
        }

        public IQueryable<Subject> GetSubjects()
        {
            return myContext.Subjects;
        }

        //Note: To practice at home, you can implement the rest of the CRUD operations.
        // If you would like to consume the C U D, repeat the steps for the Student.
    }
}
