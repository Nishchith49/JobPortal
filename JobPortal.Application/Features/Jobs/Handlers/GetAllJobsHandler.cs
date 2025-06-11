using JobPortal.Application.Features.Jobs.Models;
using JobPortal.Application.Features.Jobs.Queries;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Jobs.Handlers
{
    internal class GetAllJobsHandler : IRequestHandler<GetAllJobsQuery, PagedResponse<List<JobModel>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllJobsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<List<JobModel>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jobs
                                       .Where(x => string.IsNullOrWhiteSpace(request.Skills) || x.SkillsRequired.Contains(x.SkillsRequired))
                                       .Where(x => string.IsNullOrWhiteSpace(request.Location) || x.Location.Contains(request.Location))
                                       .Where(x => string.IsNullOrWhiteSpace(request.JobType) || x.JobType.Contains(request.JobType))
                                       .Where(x => string.IsNullOrWhiteSpace(request.FormattedSearchString()) ||
                                                   x.Title.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.SkillsRequired.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.Location.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.JobType.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.ExperienceRequired.ToString().Replace(" ", string.Empty).Contains(request.FormattedSearchString()))
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<JobModel>>
                                       {
                                           TotalRecords = x.Count(),
                                           Data = x.Select(x => new JobModel
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
                                               ExpiryDate = x.ExpiryDate,
                                               LastUpdatedDate = x.UpdatedDate,
                                               ClientCompanyId = x.ClientCompanyId
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
