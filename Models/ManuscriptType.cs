namespace FacultyJournal.Models
{
    public class ManuscriptType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
