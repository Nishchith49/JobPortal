using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Application.Features.Courses.Commands;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Courses.Handlers
{
    internal class CreateCourseHandler : IRequestHandler<CreateCourseCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateCourseHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            bool isDuplicate = await _context.Courses
                                             .AnyAsync(x => x.Title
                                                             .Replace(" ", string.Empty)
                                                             .ToLower()
                                                             .Equals(request.Title
                                                                            .Replace(" ", string.Empty)
                                                                            .ToLower()), 
                                                       cancellationToken);
            if (isDuplicate)
                return new(ResponseConstants.DuplicateCourse, 400);

            var entity = new Course
            {
                Title = request.Title,
                Description = request.Description,  
                DurationInWeeks = request.DurationInWeeks,
            };

            _context.Courses.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}