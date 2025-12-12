using Microsoft.AspNetCore.Authorization;
using NotesApp.Application.Authorization.Requirements;
using NotesApp.Application.Common.Constants;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Authorization.Handlers;

public class MustBeNoteOwnerHandler : AuthorizationHandler<MustBeNoteOwnerRequirement, Note>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MustBeNoteOwnerRequirement requirement,
        Note note
    )
    {
        var userId = context.User.FindFirst(ClaimNames.UserId)?.Value;

        if (userId is null)
        {
            return Task.CompletedTask;
        }

        if (note.UserId.ToString() == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
