using JobPortal.Application.Features.Accounts.Commands;
using JobPortal.Application.Features.Accounts.Models;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Concrete.IServices;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Global;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JobPortal.Application.Features.Accounts.Handlers
{
    internal class LoginHandler : EncryptionMethods, IRequestHandler<LoginCommand, ServiceResponse<LoginResponse>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailServices;

        public LoginHandler(ApplicationDbContext context, IConfiguration configuration, IEmailServices emailServices)
        {
            _context = context;
            _configuration = configuration;
            _emailServices = emailServices;
        }

        public async Task<ServiceResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                     .Include(x => x.UserRoles)
                                      .ThenInclude(x => x.Role)
                                     .Include(x => x.RefreshTokens)
                                     .Where(x => x.EmailId
                                                  .Replace(" ", string.Empty)
                                                  .ToLower()
                                                  .Equals(request.Email
                                                                 .ToLower()
                                                                 .Replace(" ", string.Empty)))
                                     .FirstOrDefaultAsync(cancellationToken);
            if (user is null)
                return new(ResponseConstants.UserNotExists, 401);

            if (user.Password != Encipher(request.Password))
                return new(ResponseConstants.InvalidPassword, 400);

            var loginResponse = await GetLoginResponse(user, cancellationToken);

            return new(ResponseConstants.Success, 200, loginResponse);
        }

        private async Task<LoginResponse> GetLoginResponse(User user, CancellationToken cancellationToken)
        {
            var refreshToken = GenerateRefreshToken(user.Id);
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);

            var roles = user.UserRoles.Select(x => x.Role.Name).ToList();
            var roleIds = user.UserRoles.Select(x => x.RoleId).ToList();

            var response = new LoginResponse
            {
                AccessToken = AccessToken(user, roles, roleIds),
                ExpiresIn = 3600,
                RefreshToken = refreshToken.Token,
                Roles = roles,
                TokenType = "Bearer",
                Username = user.UserName ?? string.Empty
            };

            await _context.SaveChangesAsync(cancellationToken);
            return response;
        }

        private RefreshToken GenerateRefreshToken(Guid userId)
        {
            // Type or member is obsolete
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
#pragma warning restore SYSLIB0023 // Type or member is obsolete
            // Type or member is obsolete
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.Now.AddDays(7),
                UserId = userId
            };
        }

        protected string AccessToken(User appUser, List<string> roles, List<Guid> roleIds)
        {
            var key = Encoding.ASCII.GetBytes(
                _configuration.GetValue<string>("JwtOptions:SecurityKey")
                ?? throw new InvalidOperationException("JWT SecurityKey is not configured"));

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, appUser.EmailId ?? string.Empty),
                new(ClaimTypes.Name, appUser.UserName ?? string.Empty),
                new(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new("roles", JsonConvert.SerializeObject(roles)),
                new("role_ids", JsonConvert.SerializeObject(roleIds))
            };

            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>("JwtOptions:Issuer"),
                Audience = _configuration.GetValue<string>("JwtOptions:Audience"),
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
