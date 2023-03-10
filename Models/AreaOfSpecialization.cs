using System.ComponentModel.DataAnnotations;

namespace FacultyJournal.Models
{
    public class AreaOfSpecialization
    {
        [Key]
        public int Id { get; set; }
        public string Area { get; set; }
    }
}
