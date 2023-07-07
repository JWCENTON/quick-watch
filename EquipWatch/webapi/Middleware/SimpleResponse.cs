namespace webapi.Middleware;

public class SimpleResponse
{
    public string Message { get; set; }

    public SimpleResponse(string message)
    {
        Message = message;
    }
}