using JobPortal.Application.Features.Applicants.Models;
using JobPortal.Application.Features.Applicants.Queries;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Applicants.Handlers
{
    internal class GetAllApplicantsHandler : IRequestHandler<GetAllApplicantsQuery, PagedResponse<List<ApplicantModel>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllApplicantsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<List<ApplicantModel>>> Handle(GetAllApplicantsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Applicants
                                       .Where(x => request.ClientCompanyId == null || 
                                                   x.Job.ClientCompanyId == request.ClientCompanyId)
                                       .Where(x => request.JobId == null ||
                                                   x.Job.Id == request.JobId)
                                       .Where(x => string.IsNullOrWhiteSpace(request.FormattedSearchString()) || 
                                                   x.FullName.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.Email.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.Phone.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()))
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<ApplicantModel>>
                                       {
                                           TotalRecords = x.Count(),
                                           Data = x.Select(x => new ApplicantModel
                                           {
                                               Id = x.Id,
                                               ClientCompanyName = x.Job.ClientCompany.CompanyName,
                                               JobTitle = x.Job.Title,
                                               FullName = x.FullName,
                                               Email = x.Email,
                                               Phone = x.Phone,
                                               ResumeUrl = x.ResumeUrl,
                                               Skills = x.Skills,
                                               Experience = x.Experience,
                                               JobId = x.JobId,
                                               AppliedDate = x.CreatedDate,
                                               LastUpdatedDate = x.UpdatedDate,
                                               Status = x.Status,
                                               StatusName = x.StatusName
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