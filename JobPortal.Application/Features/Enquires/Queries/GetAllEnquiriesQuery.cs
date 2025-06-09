using JobPortal.Application.Features.Enquires.Models;
using JobPortal.Infrastructure.Global;
using MediatR;

namespace JobPortal.Application.Features.Enquires.Queries
{
    public class GetAllEnquiriesQuery : PagedResponseInput, IRequest<PagedResponse<List<EnquiryModel>>>
    {
    }
}
