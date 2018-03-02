using System.Collections.Generic;
using System.Threading.Tasks;
using CallForPapers.Web.Features.Submissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CallForPapers.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty] public SubmissionInput Submission { get; set; }

        public IEnumerable<SubmissionResult> Submissions { get; set; }

        public async Task OnGetAsync()
        {
            Submissions = await mediator.Send(new GetAllSubmissionsRequest());
        }

        public async Task OnPostAsync()
        {
            await mediator.Send(Submission);
            await OnGetAsync();
        }
    }
}