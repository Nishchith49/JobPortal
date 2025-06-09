using MediatR;
using JobPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using JobPortal.Application.Features.ClientCompanies.Commands;
using JobPortal.Infrastructure.Global;

namespace JobPortal.Application.Features.ClientCompanies.Handlers
{
    internal class DeleteClientCompanyHandler : IRequestHandler<DeleteClientCompanyCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public DeleteClientCompanyHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(DeleteClientCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClientCompanies.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.IsDeleted = true;

            _context.ClientCompanies.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}