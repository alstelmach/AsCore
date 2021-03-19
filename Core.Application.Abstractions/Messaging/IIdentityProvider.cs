using System.Security.Claims;

namespace Core.Application.Abstractions.Messaging
{
    public interface IIdentityProvider
    {
        ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
