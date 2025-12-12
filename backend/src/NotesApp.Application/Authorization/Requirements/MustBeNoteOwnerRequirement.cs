using Microsoft.AspNetCore.Authorization;

namespace NotesApp.Application.Authorization.Requirements;

public class MustBeNoteOwnerRequirement : IAuthorizationRequirement
{
}
