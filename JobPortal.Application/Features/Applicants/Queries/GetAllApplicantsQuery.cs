using MediatR;
using JobPortal.Application.Features.Applicants.Models;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.Applicants.Queries
{
    public class GetAllApplicantsQuery : PagedResponseInput, IRequest<PagedResponse<List<ApplicantModel>>>
    {
    }
}