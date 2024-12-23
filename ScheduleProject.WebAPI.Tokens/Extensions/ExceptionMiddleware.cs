using Services.Validation.Exceptions;
using System.Net;

namespace TokenService.Extentions
{
    public class ExceptionMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var error = new ErrorDetails();

                error.ErrorMessage = ex.Message;
                error.Source = ex.Source;

                switch (ex)
                {
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case CustomException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.ErrorMessage = "Internal error occured. We will fix it as fast as we can.";
                        break;
                }

                var statusCode = (HttpStatusCode)context.Response.StatusCode;

                await context.Response.WriteAsync(error.ToString());
            }
        }
    }
}
