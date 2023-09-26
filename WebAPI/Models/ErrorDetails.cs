namespace WebAPI.Models;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? Details { get; set; }
    public Dictionary<string, string[]>? ValidationErrors
    {
        get;
        set;
    }

    public ErrorDetails(int statusCode, string message, string details)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }
}
