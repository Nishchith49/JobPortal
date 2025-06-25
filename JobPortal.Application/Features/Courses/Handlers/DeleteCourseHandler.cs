using JobPortal.Application.Features.Courses.Commands;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Courses.Handlers
{
    internal class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCourseHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Courses.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.IsDeleted = true;

            _context.Courses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}