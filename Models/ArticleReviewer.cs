namespace FacultyJournal.Models
{
    public class ArticleReviewer
    {
        [Key]
        public int Id { get; set; }
        
        public int ArticleId { get; set; }
        public string Title { get; set; }

        [ForeignKey("Reviewers")]
        [Required]
        public string ReviewerEmail { get; set; }
        public virtual Reviewer Reviewers { get; set; }
        public int ReviewerId { get; set; }
        public DateTime DateAssigned { get; set; } = DateTime.Now;

    }
}
