using JobPortal.Application.Features.Jobs.Models;
using JobPortal.Application.Features.Jobs.Queries;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "JobPortal User")]
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
    }
}
