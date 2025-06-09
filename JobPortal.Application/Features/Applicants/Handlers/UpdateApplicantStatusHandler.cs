using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;

namespace JobPortal.Application.Features.Applicants.Handlers
{
    internal class UpdateApplicantStatusHandler : IRequestHandler<UpdateApplicantStatusCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public UpdateApplicantStatusHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(UpdateApplicantStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Applicants.FindAsync(request.Id);
            if (entity == null)
                return new (ResponseConstants.InvalidId, 400);

            if (!Enum.IsDefined(typeof(ApplicantStatus), request.Status))
                return new(ResponseConstants.InvalidStatus, 400);

            entity.Status = (int)(ApplicantStatus)request.Status;
            entity.StatusName = Enum.GetName(typeof(ApplicantStatus), request.Status)!;
            entity.UpdatedDate = DateTime.UtcNow;

            _context.Applicants.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
