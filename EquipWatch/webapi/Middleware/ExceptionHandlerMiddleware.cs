using Newtonsoft.Json;
using System.Net;

namespace webapi.Middleware;

public class ExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
{

    public ExceptionHandlerMiddleware(RequestDelegate next) : base(next)
    {
    }

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        HttpStatusCode code;
        //OrderValidatorException, NoSuchUserException EntityAlreadyExists UserBlockedException
        switch (exception)
        {
            case KeyNotFoundException
                or FileNotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                break;
            case InvalidOperationException:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }

        return (code, JsonConvert.SerializeObject(new SimpleResponse( exception.Message == string.Empty ? GeneralError : exception.Message)));
    }
}