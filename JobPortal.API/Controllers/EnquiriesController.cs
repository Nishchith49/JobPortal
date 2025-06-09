using JobPortal.Application.Features.Enquires.Commands;
using JobPortal.Application.Features.Enquires.Models;
using JobPortal.Application.Features.Enquires.Queries;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnquiriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<EnquiryModel>>> GetEnquiries(GetAllEnquiriesQuery query)
        {
            return await _mediator.Send(query);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddEnquiry(CreateEnquiryCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpGet("[action]")]
        public async Task<APIResponse> ResolveEnquiry(Guid id)
        {
            return await _mediator.Send(new ResolveEnquiryCommand(id));
        }
    }
}
