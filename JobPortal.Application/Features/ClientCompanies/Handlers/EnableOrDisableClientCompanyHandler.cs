using JobPortal.Application.Features.ClientCompanies.Commands;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;

namespace JobPortal.Application.Features.ClientCompanies.Handlers
{
    internal class EnableOrDisableClientCompanyHandler : IRequestHandler<EnableOrDisableClientCompanyCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public EnableOrDisableClientCompanyHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(EnableOrDisableClientCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClientCompanies.FindAsync(request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.IsActive = !entity.IsActive;

            _context.ClientCompanies.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
