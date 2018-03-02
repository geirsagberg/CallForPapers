using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CallForPapers.Web.Features.Submissions
{
    public class SubmissionInput : IRequest<SubmissionResult>
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Abstract { get; set; }
    }
}