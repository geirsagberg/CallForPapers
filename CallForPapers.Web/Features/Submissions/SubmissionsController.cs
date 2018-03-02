using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapers.Web.Features.Submissions
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubmissionsController
    {
        private readonly IMediator mediator;

        public SubmissionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public Task<SubmissionResult> PostSubmission(SubmissionInput input)
        {
            return mediator.Send(input);
        }

        [HttpGet]
        public Task<SubmissionResult[]> ListAllSubmissions()
        {
            return mediator.Send(new GetAllSubmissionsRequest());
        }
    }
}