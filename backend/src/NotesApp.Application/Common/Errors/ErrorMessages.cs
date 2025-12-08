namespace NotesApp.Application.Common.Errors;

public static class ErrorMessages
{
    public const string UserNotFound = "No user exists with the provided credentials.";
    public const string UserNotFoundWithID = "No user exists with that ID.";
    public const string UserNotFoundWithEmail = "No user exists with that email.";
    public const string UserNotFoundWithUsername = "No user exists with that username.";

    public const string NoteNotFoundWithID = "No note exists with that ID.";

    public const string InvalidInput = "The provided input is invalid.";
    public const string InvalidPasswordInput = "The provided password is invalid.";
    public const string InvalidEmailInput = "The provided email is invalid.";
    public const string InvalidUsernameInput = "The provided username is invalid.";

    public const string EmailExists = "Email is already taken.";
    public const string UsernameExists = "Username is already taken.";
    public const string EmailAndOrUsernameExists = "Email and/or username is already taken.";

    public const string CreationFailed = "Failed to create the resource.";
    public const string UpdateFailed = "Failed to update the resource.";

    public const string InvalidCredentials = "Invalid login credentials.";
    public const string UnauthorizedAccess = "You are not authorized to access this resource.";
}
