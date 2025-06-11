using JobPortal.Application.Features.Jobs.Commands;
using JobPortal.Infrastructure.Global;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Jobs.Handlers
{
    internal class CreateJobHandler : IRequestHandler<CreateJobCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateJobHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            bool companyExists = await _context.ClientCompanies
                                               .AnyAsync(x => x.Id == request.ClientCompanyId, cancellationToken);
            if (!companyExists)
                return new(ResponseConstants.CompanyNotFound, 400);

            bool isDuplicate = await _context.Jobs
                                             .AnyAsync(x => x.ClientCompanyId == request.ClientCompanyId &&
                                                            x.Title.Replace(" ", "").ToLower() == request.Title.Replace(" ", "").ToLower() &&
                                                            x.Location.Replace(" ", "").ToLower() == request.Location.Replace(" ", "").ToLower(), 
                                                            cancellationToken);
            if (isDuplicate)
                return new(ResponseConstants.DuplicateJob, 400);

            var entity = new Job
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                SkillsRequired = request.SkillsRequired,
                ExperienceRequired = request.ExperienceRequired,
                JobType = request.JobType,
                Salary = request.Salary,
                ExpiryDate = request.ExpiryDate,
                ClientCompanyId = request.ClientCompanyId
            };

            _context.Jobs.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
