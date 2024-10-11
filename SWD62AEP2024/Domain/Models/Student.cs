using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Student
    {
        [Key]
        public int IdCard { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //This is the foreign key so this contains a single value
        public string GroupFK { get; set; }
        //This is a navigational propery
        //This will allow us to explore and navigate the
        //group properties right through an eventual student instance
        //adv: I can get data related to the Group pertaining to this student without having to write additional sql/linq statements
        [ForeignKey("GroupFK")]
        public Group Group { get; set; }
    }
}
