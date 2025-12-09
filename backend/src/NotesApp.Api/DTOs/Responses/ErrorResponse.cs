namespace NotesApp.Api.DTOs.Responses;

public class ErrorResponse : ApiResponseBase
{
    public string ErrorType { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    public ErrorResponse(string errorType, string errorMessage)
    {
        Success = false;
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }
}
