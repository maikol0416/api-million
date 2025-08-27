using Api.Base;
using Domain.Common;
using Domain.Entities;
using Newtonsoft.Json;
using Util.Common;
using Util.Ex;
using Utilidades;

namespace Api.Common.MiddleException
{
    public class MiddleHandlerException
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<MiddleHandlerException> _logger;

        public MiddleHandlerException(RequestDelegate next, ILogger<MiddleHandlerException> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionResponseApi(context, ex);
            }
        }

        protected Task ExceptionResponseApi(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = Constants.ContentType;
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ResponseApi<object>
            {
                Status = false,
                Data = $"{exception?.Message} --inner-- {exception?.InnerException?.ToString() ?? string.Empty}",
                Message = Constants.MessageFail
            };
            if (exception?.GetType() == typeof(DomainException))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var ex = (DomainException)exception;
                response = new ResponseApi<object> { Status = false, Data = new object(), Message = ex.Message };
            }

            if (exception?.GetType() == typeof(DomainModelExceptions))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response = new ResponseApi<object> { Status = false, Data = new object(), Message = exception.Message };
            }

            if (exception?.GetType() == typeof(UnauthorizedAccessException))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var ex = (UnauthorizedAccessException)exception;
                response = new ResponseApi<object> { Status = false, Data = Constants.MessageUnauthorized, Message = ex.Message  };
            }
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
