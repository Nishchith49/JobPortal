using JobPortal.Application.Features.ClientCompanies.Commands;
using JobPortal.Application.Features.ClientCompanies.Models;
using JobPortal.Application.Features.ClientCompanies.Queries;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientCompaniesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<ClientCompanyModel>>> GetClientCompanies(GetAllClientCompaniesQuery query)
        {
            return await _mediator.Send(query);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<ClientCompanyModel>> GetClientCompany(Guid id)
        {
            return await _mediator.Send(new GetClientCompanyByIdQuery(id));
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddClientCompany(CreateClientCompanyCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPut("[action]")]
        public async Task<APIResponse> UpdateClientCompany(UpdateClientCompanyCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteClientCompany(Guid id)
        {
            return await _mediator.Send(new DeleteClientCompanyCommand(id));
        }
    }
}