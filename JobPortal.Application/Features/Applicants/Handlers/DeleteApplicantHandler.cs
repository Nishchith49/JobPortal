using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Applicants.Handlers
{
    internal class DeleteApplicantHandler : IRequestHandler<DeleteApplicantCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public DeleteApplicantHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Applicants.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.IsDeleted = true;

            _context.Applicants.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}