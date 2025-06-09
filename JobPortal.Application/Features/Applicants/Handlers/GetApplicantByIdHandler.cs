using JobPortal.Application.Features.Applicants.Models;
using JobPortal.Application.Features.Applicants.Queries;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Applicants.Handlers
{
    internal class GetApplicantByIdHandler : IRequestHandler<GetApplicantByIdQuery, ServiceResponse<ApplicantModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetApplicantByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ApplicantModel>> Handle(GetApplicantByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Applicants
                                       .Select(x => new ApplicantModel
                                       {
                                           Id = x.Id,
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
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return new(ResponseConstants.Success, 200, entity);
        }
    }
}