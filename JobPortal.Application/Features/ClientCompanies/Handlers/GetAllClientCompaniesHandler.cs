using JobPortal.Application.Features.ClientCompanies.Models;
using JobPortal.Application.Features.ClientCompanies.Queries;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.ClientCompanies.Handlers
{
    internal class GetAllClientCompaniesHandler : IRequestHandler<GetAllClientCompaniesQuery, PagedResponse<List<ClientCompanyModel>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllClientCompaniesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<List<ClientCompanyModel>>> Handle(GetAllClientCompaniesQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClientCompanies
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<ClientCompanyModel>>
                                       {
                                             TotalRecords = x.Count(),
                                             Data = x.Select(x => new ClientCompanyModel
                                             {
                                                  Id = x.Id,
                                                  CompanyName = x.CompanyName,
                                                  ContactPersonName = x.ContactPersonName,
                                                  ContactEmail = x.ContactEmail,
                                                  ContactPhone = x.ContactPhone,
                                                  Designation = x.Designation,
                                                  CompanyWebsite = x.CompanyWebsite,
                                                  LinkedInProfile = x.LinkedInProfile,
                                                  Location = x.Location,
                                                  Notes = x.Notes,
                                                  IsActive = x.IsActive,
                                                  CreatedDate = x.CreatedDate,
                                                  LastUpdatedDate = x.UpdatedDate
                                             })
                                             .Skip(request.PageSize * request.PageIndex)
                                             .Take(request.PageSize)
                                             .ToList()
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);

            return new(ResponseConstants.Success, 200, entity?.Data, request.PageIndex, request.PageSize, entity?.TotalRecords ?? 0);
        }
    }
}