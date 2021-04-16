using System.Security.Claims;

namespace ASCore.Application.Abstractions.Messaging
{
    public interface IIdentityProvider
    {
        ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
