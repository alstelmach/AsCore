using System.Security.Claims;

namespace AsCore.Application.Abstractions.Messaging
{
    public interface IIdentityProvider
    {
        ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
