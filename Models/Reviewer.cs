namespace FacultyJournal.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string? OtherName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
       
        [Key]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Affiliation { get; set; }

        [ForeignKey("AreasOfSpecialization")]
        [Required]
        public int AreaOfSpecializationId { get; set; }
        public virtual AreaOfSpecialization AreasOfSpecialization { get; set; }

       

    }
}
