namespace FacultyJournal.Models
{
    public class ApplicationUser: IdentityUser
    {
        
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? OtherName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        //public string Email { get; set; }
        public string? Affiliation { get; set; }
    }
}
