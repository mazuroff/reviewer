using Reviewer.Core.Interfaces;
using Reviewer.Core.Models;
using Reviewer.DAL.Entities;
using Reviewer.DAL.Interfaces;
using System.Linq;
using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Reviewer.Core.Options;
using Microsoft.Extensions.Options;

namespace Reviewer.Core.Infrastructure
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICryptoContext cryptoContext;
        private readonly JwtOptions options;

        public TokenService(IUnitOfWork _unitOfWork, IOptions<JwtOptions> _options, ICryptoContext _cryptoContext)
        {
            unitOfWork = _unitOfWork;
            options = _options.Value;
            cryptoContext = _cryptoContext;
        }

        public TokenModel GetToken(LoginCredentials loginCredentials)
        {
            var user = unitOfWork.Repository<UserEntity>()
                .Set
                .FirstOrDefault(x => x.Email == loginCredentials.Email);

            if (user == null)
            {
                return null;
            }

            if (cryptoContext.ArePasswordsEqual(loginCredentials.Password, user.Password, user.Salt))
            {
                return BuildToken(user);
            }

            return null;
        }

        private TokenModel BuildToken(UserEntity user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
                };

            var token = new JwtSecurityToken(
              issuer: options.ValidIssuer,
              audience: options.ValidAudience,
              claims: claims,
              expires: DateTime.Now.AddMinutes(options.Lifetime),
              signingCredentials: creds);
            
            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = DateTimeOffset.Now.AddMinutes(options.Lifetime),
                IssueAt = DateTimeOffset.Now,
                Id = user.Id,
                UserType = user.Type
            };
        }
    }
}
