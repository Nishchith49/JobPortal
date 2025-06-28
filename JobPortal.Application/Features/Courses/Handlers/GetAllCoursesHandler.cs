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
    internal class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, PagedResponse<List<CourseModel>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllCoursesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<List<CourseModel>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Courses
                                       .Where(x => string.IsNullOrWhiteSpace(request.FormattedSearchString()) ||
                                                   x.Title.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()))
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<CourseModel>>
                                       {
                                           TotalRecords = x.Count(),
                                           Data = x.Select(x => new CourseModel
                                           {
                                               Id = x.Id,
                                               Title = x.Title,
                                               Description = x.Description,
                                               DurationInWeeks = x.DurationInWeeks,
                                               CreatedDate = x.CreatedDate,
                                               LastUpdatedDate = x.UpdatedDate
                                           })
                                           .Skip(request.PageSize * request.PageIndex)
                                           .Take(request.PageSize)
                                           .ToList()
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);

            return new(ResponseConstants.Success, 200, entity?.Data, request.PageIndex, request.PageSize, entity?.TotalRecords ?? 0);
        }
    }
}