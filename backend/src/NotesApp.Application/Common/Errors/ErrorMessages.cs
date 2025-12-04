namespace NotesApp.Application.Common.Errors;

public static class ErrorMessages
{
    public const string UserNotFoundWithID = "No user exists with that ID.";
    public const string UserNotFoundWithEmail = "No user exists with that email.";
    public const string UserNotFoundWithUsername = "No user exists with that username.";

    public const string NoteNotFoundWithID = "No note exists with that ID.";

    public const string InvalidInput = "The provided input is invalid.";
    public const string InvalidEmailInput = "The provided email is invalid.";
    public const string InvalidUsernameInput = "The provided username is invalid.";

    public const string EmailTaken = "Email is already taken.";
    public const string UsernameTaken = "Username is already taken.";
    public const string EmailAndOrUsernameTaken = "Email and/or username is already taken.";

    public const string CreationFailed = "Failed to create the resource.";
    public const string UpdateFailed = "Failed to update the resource.";

    public const string InvalidCredentials = "Invalid login credentials.";
    public const string UnauthorizedAccess = "You are not authorized to access this resource.";
}
