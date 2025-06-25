using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Applicants.Handlers
{
    internal class UpdateApplicantHandler : IRequestHandler<UpdateApplicantCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public UpdateApplicantHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(UpdateApplicantCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Applicants.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.FullName = request.FullName;
            entity.Email = request.Email;
            entity.Phone = request.Phone;
            entity.ResumeUrl = request.ResumeUrl;
            entity.Skills = request.Skills;
            entity.Experience = request.Experience;
            entity.JobId = request.JobId;
            entity.UpdatedDate = DateTime.UtcNow;

            _context.Applicants.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}