using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Applicants.Handlers
{
    internal class CreateApplicantHandler : IRequestHandler<CreateApplicantCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateApplicantHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
        {
            bool isDuplicate = await _context.Applicants
                                             .AnyAsync(x => x.JobId == request.JobId && 
                                                           (x.Email == request.Email ||
                                                            x.Phone == request.Phone), 
                                                            cancellationToken);
            if (isDuplicate)
                return new(ResponseConstants.DuplicateApplicant, 400);

            var entity = new Applicant
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                ResumeUrl = request.ResumeUrl,
                Skills = request.Skills,
                Experience = request.Experience,
                JobId = request.JobId,
                Status = (int)ApplicantStatus.Applied,
                StatusName = nameof(ApplicantStatus.Applied)
            };

            _context.Applicants.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}