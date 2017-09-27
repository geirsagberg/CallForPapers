using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CallForPapers.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CallForPapers.Web.Features.Submission
{
    [Route("api/[controller]")]
    public class SubmissionController : Controller
    {
        private readonly SubmissionContext context;
        private readonly ILogger<SubmissionController> logger;

        public SubmissionController(SubmissionContext context, ILogger<SubmissionController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult PostSubmission([FromBody] SubmissionInput input)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            context.Add(Mapper.Map<Data.Entities.Submission>(input));
            context.SaveChanges();
            logger.LogInformation("New submission from {FirstName} {LastName}", input.FirstName, input.LastName);
            return Ok();
        }

        [HttpGet]
        public IList<SubmissionResult> ListAllSubmissions() =>
            context.Submissions.ProjectTo<SubmissionResult>().ToList();
    }

    public class SubmissionResult
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
    }

    public class SubmissionInput
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Abstract { get; set; }
    }
}