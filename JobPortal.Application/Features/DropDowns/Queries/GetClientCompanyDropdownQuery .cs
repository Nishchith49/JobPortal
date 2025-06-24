using JobPortal.Application.Features.DropDowns.Models;
using MediatR;

namespace JobPortal.Application.Features.DropDowns.Queries
{
    public class GetClientCompanyDropdownQuery : IRequest<List<DropDownModel>>
    {
    }
}

