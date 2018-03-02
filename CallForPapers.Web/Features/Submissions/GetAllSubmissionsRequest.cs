using MediatR;

namespace CallForPapers.Web.Features.Submissions
{
    public class GetAllSubmissionsRequest : IRequest<SubmissionResult[]>
    {
    }
}