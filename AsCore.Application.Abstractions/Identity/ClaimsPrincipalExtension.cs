using System;
using System.Linq;
using System.Security.Claims;

namespace AsCore.Application.Abstractions.Identity
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetOwnerId(this ClaimsPrincipal claimsPrincipal) =>
            Guid.Parse(claimsPrincipal
                 .Claims
                 .FirstOrDefault(claim =>
                     claim.Type == ClaimTypes.OwnerId)
                 ?.Value
                 ?? string.Empty);
    }
}
