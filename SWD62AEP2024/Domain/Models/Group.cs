using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Group
    {
        [Key]
        public string Code { get; set; }
        public string Programme { get; set; }
    }
}
