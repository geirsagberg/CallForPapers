using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CallForPapers.Web.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CallForPapers.Web.Features.Submissions
{
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
}