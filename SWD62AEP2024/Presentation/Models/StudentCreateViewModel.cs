using Domain.Models;

namespace Presentation.Models
{

    //ViewModels are views which help transfer data to and from the Views
    //Models are models which will be used to design and generate the database (should be kept more secure and up-to-date)

    public class StudentCreateViewModel
    {
        public Student Student { get; set; }

        public List<Group> Groups { get; set; }
    }
}
