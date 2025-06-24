using JobPortal.Application.Features.DropDowns.Models;
using JobPortal.Application.Features.DropDowns.Queries;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.DropDowns.Handlers
{
    internal class GetClientCompanyDropdownHandler : IRequestHandler<GetClientCompanyDropdownQuery, List<DropDownModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetClientCompanyDropdownHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DropDownModel>> Handle(GetClientCompanyDropdownQuery request, CancellationToken cancellationToken)
        {
            return await _context.ClientCompanies
                                 .OrderBy(x => x.CompanyName)
                                 .Select(x => new DropDownModel
                                 {
                                     Label = x.CompanyName,
                                     Value = x.Id
                                 }).ToListAsync(cancellationToken);
        }
    }
}
