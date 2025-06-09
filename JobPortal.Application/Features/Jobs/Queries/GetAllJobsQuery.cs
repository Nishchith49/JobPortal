using MediatR;
using JobPortal.Application.Features.Jobs.Models;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.Jobs.Queries
{
    public class GetAllJobsQuery : PagedResponseInput, IRequest<PagedResponse<List<JobModel>>>
    {
    }
}
