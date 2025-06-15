using JobPortal.Application.Features.Accounts.Commands;
using JobPortal.Application.Features.Accounts.Models;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<ServiceResponse<LoginResponse>> Login(LoginCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
