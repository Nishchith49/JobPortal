using JobPortal.Application.Features.ClientCompanies.Models;
using JobPortal.Application.Features.ClientCompanies.Queries;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.ClientCompanies.Handlers
{
    internal class GetClientCompanyByIdHandler : IRequestHandler<GetClientCompanyByIdQuery, ServiceResponse<ClientCompanyModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetClientCompanyByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ClientCompanyModel>> Handle(GetClientCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClientCompanies
                                       .Select(x => new ClientCompanyModel
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
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return new(ResponseConstants.Success, 200, entity);
        }
    }
}