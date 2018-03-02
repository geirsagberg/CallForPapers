using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CallForPapers.Web.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

    public class GetAllSubmissionsRequest : IRequest<SubmissionResult[]>
    {
    }

    public class SubmissionHandler : IRequestHandler<SubmissionInput, SubmissionResult>,
        IRequestHandler<GetAllSubmissionsRequest, SubmissionResult[]>
    {
        private readonly SubmissionsContext context;
        private readonly ILogger<SubmissionHandler> logger;

        public SubmissionHandler(SubmissionsContext context, ILogger<SubmissionHandler> logger
        )
        {
            this.context = context;
            this.logger = logger;
        }

        public Task<SubmissionResult[]> Handle(GetAllSubmissionsRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Submissions.ProjectTo<SubmissionResult>().ToArray());
        }

        public async Task<SubmissionResult> Handle(SubmissionInput input, CancellationToken cancellationToken)
        {
            var submission = new Submission
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Title = input.Title,
                Abstract = input.Abstract
            };

            context.Add(submission);

            await context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("New submission from {FirstName} {LastName}", input.FirstName, input.LastName);

            return Mapper.Map<SubmissionResult>(submission);
        }
    }

    public class SubmissionResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
    }

    public class SubmissionInput : IRequest<SubmissionResult>
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Abstract { get; set; }
    }
}