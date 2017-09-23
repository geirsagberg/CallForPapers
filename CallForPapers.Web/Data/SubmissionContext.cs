using CallForPapers.Web.Data.Entities;
using CallForPapers.Web.Features.Submission;
using Microsoft.EntityFrameworkCore;

namespace CallForPapers.Web.Data
{
    public class SubmissionContext : DbContext
    {
        public SubmissionContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Submission> Submissions { get; set; }
        
    }
}