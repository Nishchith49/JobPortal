using JobPortal.Application.Features.Enquires.Commands;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "JobPortal User")]
    public class EnquiriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<APIResponse> AddEnquiry(CreateEnquiryCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
