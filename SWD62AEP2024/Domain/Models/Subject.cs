using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Subject
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
