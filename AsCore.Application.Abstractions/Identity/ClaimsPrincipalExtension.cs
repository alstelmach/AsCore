using System.Linq;
using System.Security.Claims;

namespace AsCore.Application.Abstractions.Identity
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetClaimValue(
            this ClaimsPrincipal claimsPrincipal,
            string claimType) =>
                claimsPrincipal
                    .Claims
                    .FirstOrDefault(claim =>
                        claim.Type == claimType)
                    ?.Value
                    ?? string.Empty;
    }
}
