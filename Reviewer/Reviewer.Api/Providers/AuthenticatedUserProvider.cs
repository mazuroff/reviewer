using Microsoft.AspNetCore.Http;
using Reviewer.Core.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace Reviewer.Api.Providers
{
    public class AuthenticatedUserProvider : IAuthenticatedUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated
        {
            get
            {
                ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User;
                return principal?.Identity?.IsAuthenticated ?? false;
            }
        }

        public string Name
        {
            get
            {
                var user = _httpContextAccessor.HttpContext.User;
                return user?.Identity?.IsAuthenticated ?? false
                    ? user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value
                    : null;
            }
        }

        public Guid UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext.User;
                if (!(user?.Identity?.IsAuthenticated) ?? false)
                {
                    return default(Guid);
                }
                string nameId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
                return Guid.Parse(nameId);
            }
        }

        public string Email
        {
            get
            {
                var user = _httpContextAccessor.HttpContext.User;
                return user?.Identity?.IsAuthenticated ?? false
                    ? user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value
                    : null;
            }
        }
    }
}
