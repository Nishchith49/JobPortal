using JobPortal.Application.Features.Courses.Commands;
using JobPortal.Application.Features.Courses.Models;
using JobPortal.Application.Features.Courses.Queries;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiExplorerSettings(GroupName = "JobPortal Admin")]
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


        [HttpPost("[action]")]
        public async Task<APIResponse> AddCourse(CreateCourseCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut("[action]")]
        public async Task<APIResponse> UpdateCourse(UpdateCourseCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut("[action]")]
        public async Task<APIResponse> EnableOrDisableCourse(EnableOrDisableCourseCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteCourse(Guid id)
        {
            return await _mediator.Send(new DeleteCourseCommand(id));
        }
    }
}
