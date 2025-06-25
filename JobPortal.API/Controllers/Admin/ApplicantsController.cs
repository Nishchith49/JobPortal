using JobPortal.Application.Features.Applicants.Commands;
using JobPortal.Application.Features.Applicants.Models;
using JobPortal.Application.Features.Applicants.Queries;
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
    public class ApplicantsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<ApplicantModel>>> GetApplicants(GetAllApplicantsQuery query)
        {
            return await _mediator.Send(query);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<ApplicantModel>> GetApplicant(Guid id)
        {
            return await _mediator.Send(new GetApplicantByIdQuery(id));
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddApplicant(CreateApplicantCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut("[action]")]
        public async Task<APIResponse> UpdateApplicant(UpdateApplicantCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut("[action]")]
        public async Task<APIResponse> UpdateApplicantStatus(UpdateApplicantStatusCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteApplicant(Guid id)
        {
            return await _mediator.Send(new DeleteApplicantCommand(id));
        }
    }
}
