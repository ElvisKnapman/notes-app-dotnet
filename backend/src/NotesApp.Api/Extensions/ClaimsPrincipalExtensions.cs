using System.Security.Claims;

namespace NotesApp.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("User ID claim missing");

        if (!Guid.TryParse(userId, out Guid parsedId))
        {
            throw new InvalidOperationException("User ID claim is not a valid GUID.");
        }

        return parsedId;
    }
}
