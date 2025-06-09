using JobPortal.Application.Features.Jobs.Commands;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace JobPortal.Application.Features.Jobs.Handlers
{
    internal class DeleteJobHandler : IRequestHandler<DeleteJobCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public DeleteJobHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClientCompanies.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId,400);

            entity.IsDeleted = true;

            _context.ClientCompanies.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
