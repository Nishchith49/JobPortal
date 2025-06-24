using JobPortal.Application.Features.Accounts.Commands;
using JobPortal.Application.Features.Accounts.Models;
using JobPortal.Application.Features.DropDowns.Models;
using JobPortal.Application.Features.DropDowns.Queries;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetClientCompanyDropdown()
        {
            return await _mediator.Send(new GetClientCompanyDropdownQuery());
        }


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetJobDropDownQuery()
        {
            return await _mediator.Send(new GetJobDropDownQuery());
        }
    }
}
