using Microsoft.AspNetCore.Diagnostics;
using phone_book_app.Server.Exceptions;
using System.Net;
using System.Net.Mime;

namespace phone_book_app.Server.ExceptionHandlers
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;

            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            string type = string.Empty;

            if (exception.GetType().Name.Equals(typeof(KeyNotFoundException).Name))
            {
                statusCode = HttpStatusCode.NotFound;
                type = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                type = "https://tools.ietf.org/html/rfc9110#section-15.6.1";
            }

            var exceptions = exception.GetAllExceptions()
                .Select(ex => new Dictionary<string, IEnumerable<string>>()
                {
                    { ex.GetType().Name, new List<string>() { ex.Message } }
                });

            var errorResponse = new
            {
                Type = type,
                Title = "One or more errors occurred.",
                Status = (int)statusCode,
                Errors = exceptions,
                TraceId = httpContext.TraceIdentifier
            };

            httpContext.Response.StatusCode = (int)statusCode;

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}
