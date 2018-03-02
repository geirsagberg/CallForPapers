using Microsoft.EntityFrameworkCore;

namespace CallForPapers.Web.Data
{
    public class SubmissionsContext : DbContext
    {
        public SubmissionsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Submission> Submissions { get; set; }
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