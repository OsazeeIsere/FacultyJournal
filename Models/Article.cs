

namespace FacultyJournal.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Abstract { get; set; }
        public string? Keywords { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime DateSubmited { get; set; } = DateTime.Now;
        public string? SubmittedBy { get; set; }
        public ArticleStatus Status { get; set; } = ArticleStatus.Pending;
        [Required]
        [ForeignKey("ManuscriptTypes")]
        public int ManuscriptTypeId { get; set; }
        public virtual ManuscriptType ManuscriptTypes { get; set; }
        
        public string? UploadedFile { get; set; }
    }
}
