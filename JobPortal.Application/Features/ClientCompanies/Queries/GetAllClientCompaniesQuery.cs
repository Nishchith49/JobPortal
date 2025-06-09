using MediatR;
using JobPortal.Application.Features.ClientCompanies.Models;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.ClientCompanies.Queries
{
    public class GetAllClientCompaniesQuery : PagedResponseInput, IRequest<PagedResponse<List<ClientCompanyModel>>>
    {
    }
}