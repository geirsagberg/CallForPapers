using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CallForPapers.Web.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CallForPapers.Web.Features.Submission
{
    [Route("api/[controller]")]
    public class SubmissionController
    {
        private readonly IMediator mediator;

        public SubmissionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<ICollection<SubmissionResult>> GetSubmissions() =>
            mediator.Send(new GetSubmissionsRequest());

        [HttpPost]
        public Task SubmitPaper([FromBody]SubmissionInput submission) => mediator.Send(submission);
    }

    public class SubmissionHandler : IRequestHandler<GetSubmissionsRequest, ICollection<SubmissionResult>>,
        IRequestHandler<SubmissionInput>
    {
        private readonly SubmissionContext context;

        public SubmissionHandler(SubmissionContext context)
        {
            this.context = context;
        }

        public ICollection<SubmissionResult> Handle(GetSubmissionsRequest message) =>
            context.Submissions.ProjectTo<SubmissionResult>().ToList();

        public void Handle(SubmissionInput message)
        {
            context.Add(Mapper.Map<Data.Submission>(message));
            context.SaveChanges();
        }
    }


    public class GetSubmissionsRequest : IRequest<ICollection<SubmissionResult>>
    {
    }

    public class SubmissionInput : IRequest
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