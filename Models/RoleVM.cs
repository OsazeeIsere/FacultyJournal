using System.ComponentModel.DataAnnotations;

namespace FacultyJournal.Models
{
    public class RoleVM
    {
        [Required]
        public string? Name { get; set; }
    }
}
