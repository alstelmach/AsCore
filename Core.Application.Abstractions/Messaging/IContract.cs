using System.Security.Claims;

namespace Core.Application.Abstractions.Messaging
{
    public interface IContract
    {
        ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
