using JobPortal.Application.Features.Courses.Models;
using JobPortal.Infrastructure.Global;
using MediatR;

namespace JobPortal.Application.Features.Courses.Queries
{
    public class GetAllCoursesQuery : PagedResponseInput, IRequest<PagedResponse<List<CourseModel>>>
    {
    }
}