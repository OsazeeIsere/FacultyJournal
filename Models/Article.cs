

namespace FacultyJournal.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Abstract { get; set; }
        [Required]
        public string Keywords { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
        public DateTime DateSubmited { get; set; } = DateTime.Now;
        [Required]
        public string SubmittedBy { get; set; }
        public ArticleStatus Status { get; set; } = ArticleStatus.Pending;
        [Required]
        [ForeignKey("ManuscriptTypes")]
        public int ManuscriptTypeId { get; set; }
        public virtual ManuscriptType ManuscriptTypes { get; set; }
        
        public string? UploadedFile { get; set; }
    }
}
