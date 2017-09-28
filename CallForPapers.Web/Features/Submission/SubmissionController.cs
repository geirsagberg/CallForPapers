using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CallForPapers.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapers.Web.Features.Submission
{
    [Route("api/[controller]")]
    public class SubmissionController
    {
        private readonly SubmissionContext context;

        public SubmissionController(SubmissionContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ICollection<SubmissionResult> GetSubmissions() =>
            context.Submissions.ProjectTo<SubmissionResult>().ToList();

        [HttpPost]
        public void SubmitPaper(SubmissionInput submission)
        {
            context.Submissions.Add(Mapper.Map<Data.Submission>(submission));
            context.SaveChanges();
        }
    }

    public class SubmissionInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
    }

    public class SubmissionResult
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
    }
}