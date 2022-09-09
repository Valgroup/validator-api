using System.Net;
using Validator.API.Models;

namespace Validator.API.Middlewares
{
    public class ExceptionGlobalHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionGlobalHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var errors = new List<string>
                {
                    ex.Message
                };

                if (ex.InnerException != null)
                    errors.Add(ex.InnerException.Message);

                switch (ex)
                {
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsJsonAsync<ResponseModel>(new ResponseModel { Code = response.StatusCode, Message = string.Join(" | ", errors) });

            }
        }
    }
}
