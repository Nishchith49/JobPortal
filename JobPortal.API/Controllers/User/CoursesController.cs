using JobPortal.Application.Features.Courses.Models;
using JobPortal.Application.Features.Courses.Queries;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "JobPortal User")]
    public class CoursesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<CourseModel>>> GetCourses(GetAllCoursesQuery query)
        {
            return await _mediator.Send(query);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<CourseModel>> GetCourse(Guid id)
        {
            return await _mediator.Send(new GetCourseByIdQuery(id));
        }
    }
}
