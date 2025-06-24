using JobPortal.Application.Features.Jobs.Commands;
using JobPortal.Application.Features.Jobs.Models;
using JobPortal.Application.Features.Jobs.Queries;
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
    public class JobsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<JobModel>>> GetJobs(GetAllJobsQuery query)
        {
            return await _mediator.Send(query);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<JobModel>> GetJob(Guid id)
        {
            return await _mediator.Send(new GetJobByIdQuery(id));
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddJob(CreateJobCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut("[action]")]
        public async Task<APIResponse> UpdateJob(UpdateJobCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut("[action]")]
        public async Task<APIResponse> EnableOrDisableJob(EnableOrDisableJobCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteJob(Guid id)
        {
            return await _mediator.Send(new DeleteJobCommand(id));
        }
    }
}
