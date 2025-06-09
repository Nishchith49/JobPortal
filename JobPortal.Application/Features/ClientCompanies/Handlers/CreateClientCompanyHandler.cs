using JobPortal.Application.Features.ClientCompanies.Commands;
using JobPortal.Infrastructure.Global;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.ClientCompanies.Handlers
{
    internal class CreateClientCompanyHandler : IRequestHandler<CreateClientCompanyCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateClientCompanyHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(CreateClientCompanyCommand request, CancellationToken cancellationToken)
        {
            bool isDuplicate = await _context.ClientCompanies
                                             .AnyAsync(x => x.CompanyName
                                                             .Replace(" ", string.Empty)
                                                             .ToLower()
                                                             .Equals(request.CompanyName
                                                                            .Replace(" ", string.Empty)
                                                                            .ToLower()), 
                                                       cancellationToken);
            if (isDuplicate)
                return new(ResponseConstants.DuplicateCompany, 400);

            var entity = new ClientCompany
            {
                CompanyName = request.CompanyName,
                ContactPersonName = request.ContactPersonName,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                Designation = request.Designation,
                CompanyWebsite = request.CompanyWebsite,
                LinkedInProfile = request.LinkedInProfile,
                Location = request.Location,
                Notes = request.Notes
            };

            _context.ClientCompanies.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}