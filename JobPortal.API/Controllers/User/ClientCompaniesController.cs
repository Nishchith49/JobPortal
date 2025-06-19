using JobPortal.Application.Features.ClientCompanies.Models;
using JobPortal.Application.Features.ClientCompanies.Queries;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "JobPortal User")]
    public class ClientCompaniesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<ClientCompanyModel>>> GetClientCompanies(GetAllClientCompaniesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}