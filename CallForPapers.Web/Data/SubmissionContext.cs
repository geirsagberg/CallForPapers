using Microsoft.EntityFrameworkCore;

namespace CallForPapers.Web.Data
{
    public class SubmissionContext : DbContext
    {
        public DbSet<Submission> Submissions { get; set; }


        public SubmissionContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class Submission
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        
    }
}