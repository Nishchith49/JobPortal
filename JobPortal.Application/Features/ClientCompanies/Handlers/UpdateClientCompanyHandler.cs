using MediatR;
using JobPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using JobPortal.Application.Features.ClientCompanies.Commands;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.ClientCompanies.Handlers
{
    internal class UpdateClientCompanyHandler : IRequestHandler<UpdateClientCompanyCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public UpdateClientCompanyHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(UpdateClientCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClientCompanies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.CompanyName = request.CompanyName;
            entity.ContactPersonName = request.ContactPersonName;
            entity.ContactEmail = request.ContactEmail;
            entity.ContactPhone = request.ContactPhone;
            entity.Designation = request.Designation;
            entity.CompanyWebsite = request.CompanyWebsite;
            entity.LinkedInProfile = request.LinkedInProfile;
            entity.Location = request.Location;
            entity.Notes = request.Notes;
            entity.UpdatedDate = DateTime.UtcNow;

            _context.ClientCompanies.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}