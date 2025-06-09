using JobPortal.Application.Features.Jobs.Models;
using JobPortal.Application.Features.Jobs.Queries;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Jobs.Handlers
{
    internal class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, ServiceResponse<JobModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetJobByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<JobModel>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jobs
                                       .Select(x => new JobModel
                                       {
                                           Id = x.Id,
                                           Title = x.Title,
                                           Description = x.Description,
                                           Location = x.Location,
                                           SkillsRequired = x.SkillsRequired,
                                           ExperienceRequired = x.ExperienceRequired,
                                           JobType = x.JobType,
                                           Salary = x.Salary,
                                           IsActive = x.IsActive,
                                           PostedDate = x.CreatedDate,
                                           LastUpdatedDate = x.UpdatedDate,
                                           ClientCompanyId = x.ClientCompanyId
                                       })
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return new(ResponseConstants.Success, 200, entity);
        }
    }
}
