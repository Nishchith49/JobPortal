using JobPortal.Application.Features.Courses.Commands;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Applicants.Handlers
{
    internal class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public UpdateCourseHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new(ResponseConstants.InvalidId, 400);

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.DurationInWeeks = request.DurationInWeeks;
            entity.UpdatedDate = DateTime.UtcNow;

            _context.Courses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}