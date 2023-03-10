using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FacultyJournal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ManuscriptType> ManuscriptTypes { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<AreaOfSpecialization> AreasOfSpecialization { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<ArticleReviewer> ArticleReviewers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}