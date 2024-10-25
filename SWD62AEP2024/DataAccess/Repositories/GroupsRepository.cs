using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class GroupsRepository
    {
        private AttendanceContext myContext;

        public GroupsRepository(AttendanceContext _myContext)
        {
            myContext = _myContext;
        }

        public IQueryable<Group> GetGroups()
        {
            return myContext.Groups;
        }
    }
}
