using JobPortal.Application.Features.Jobs.Commands;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Jobs.Handlers
{
    internal class UpdateJobHandler : IRequestHandler<UpdateJobCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public UpdateJobHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            bool companyExists = await _context.ClientCompanies
                                               .AnyAsync(x => x.Id == request.ClientCompanyId, cancellationToken);
            if (!companyExists)
                return new(ResponseConstants.CompanyNotFound, 400);

            var entity = await _context.Jobs.FindAsync(request.Id);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Location = request.Location;
            entity.SkillsRequired = request.SkillsRequired;
            entity.ExperienceRequired = request.ExperienceRequired;
            entity.JobType = request.JobType;
            entity.Salary = request.Salary;
            entity.ExpiryDate = request.ExpiryDate;
            entity.ClientCompanyId = request.ClientCompanyId;
            entity.UpdatedDate = DateTime.UtcNow;

            _context.Jobs.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
