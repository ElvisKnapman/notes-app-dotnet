namespace NotesApp.Application.Common.Errors;

public static class ErrorCodes
{
    public const string UserNotFound = "user.not_found";
    public const string NoteNotFound = "note.not_found";

    public const string InvalidInput = "common.invalid_input";
    public const string UsernameExists = "user.username_exists";
    public const string EmailExists = "user.email_exists";
    public const string EmailAndOrUsernameTaken = "user.email_and_or_username_taken";

    public const string UpdateFailed = "user.update_failed";
    public const string CreationFailed = "user.creation_failed";

    public const string InvalidCredentials = "auth.invalid_credentials";
    public const string UnauthorizedAccess = "auth.unauthorized";
}
