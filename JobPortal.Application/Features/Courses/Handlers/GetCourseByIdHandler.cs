using JobPortal.Application.Features.Applicants.Models;
using JobPortal.Application.Features.Applicants.Queries;
using JobPortal.Application.Features.Courses.Models;
using JobPortal.Application.Features.Courses.Queries;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Courses.Handlers
{
    internal class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, ServiceResponse<CourseModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetCourseByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<CourseModel>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Courses
                                       .Select(x => new CourseModel
                                       {
                                           Id = x.Id,
                                           Title = x.Title,
                                           Description = x.Description,
                                           DurationInWeeks = x.DurationInWeeks,
                                           IsActive = x.IsActive,
                                           CreatedDate = x.CreatedDate,
                                           LastUpdatedDate = x.UpdatedDate
                                       })
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return new(ResponseConstants.Success, 200, entity);
        }
    }
}