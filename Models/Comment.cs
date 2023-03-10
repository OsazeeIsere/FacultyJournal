namespace FacultyJournal.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
      
        public int ArticleId { get; set; }
    
        public string? CommentBody { get; set; }
        public int ReviewerId { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
    }
}
