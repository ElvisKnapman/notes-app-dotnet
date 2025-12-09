namespace NotesApp.Api.DTOs.Responses;

public class SuccessResponse<T> : ApiResponseBase
{
    public T Data { get; set; }

    public SuccessResponse(T data)
    {
        Success = true;
        Data = data;
    }
}