using System.Security.Claims;

namespace AsCore.Application.Abstractions.Messaging
{
    public abstract record Contract : IIdentityProvider
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
