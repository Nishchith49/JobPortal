using JobPortal.Application.Features.Jobs.Commands;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;

namespace JobPortal.Application.Features.Jobs.Handlers
{
    internal class EnableOrDisableJobHandler : IRequestHandler<EnableOrDisableJobCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public EnableOrDisableJobHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(EnableOrDisableJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jobs.FindAsync(request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.IsActive = !entity.IsActive;

            _context.Jobs.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
