using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "JobPortal User")]
    public class ApplicantsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<APIResponse> AddApplicant(CreateApplicantCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
