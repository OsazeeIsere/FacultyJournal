namespace FacultyJournal.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string? OtherName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Affiliation { get; set; }
        public string ORCIDNumber { get; set; }
        public Boolean IsCorrespondingAuthor { get; set; } = false;

        [ForeignKey("Countries")]
        [Required]
        public int CountryId { get; set; }
        public virtual Country Countries { get; set; }

        [ForeignKey("Articles")]
        [Required]
        public int ArticleId { get; set; }
        public virtual Article Articles { get; set; }

        public Boolean MultipleAuthors { get; set; } = false;

    }
}
