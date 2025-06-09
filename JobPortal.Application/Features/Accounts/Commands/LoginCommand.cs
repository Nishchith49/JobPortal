using JobPortal.Application.Features.Accounts.Models;
using JobPortal.Infrastructure.Global;
using MediatR;
using Newtonsoft.Json;

namespace JobPortal.Application.Features.Accounts.Commands
{
    public class LoginCommand : IRequest<ServiceResponse<LoginResponse>>
    {
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        //public LoginCommand(string email, string password)
        //{
        //    Email = email;
        //    Password = password;
        //}
    }
}
