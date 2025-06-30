using JobPortal.Application.Features.Courses.Commands;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;

namespace JobPortal.Application.Features.Courses.Handlers
{
    internal class EnableOrDisableCourseHandler : IRequestHandler<EnableOrDisableCourseCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public EnableOrDisableCourseHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(EnableOrDisableCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Courses.FindAsync(request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.IsActive = !entity.IsActive;

            _context.Courses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
