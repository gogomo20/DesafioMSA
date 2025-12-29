using DesafioMSA.Application.Responses;
using DesafioMSA.Domain.Shared.Exceptions;

namespace DesafioMSA.Presentation.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(NotFoundedExeption notFounded)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new GenericResponseNoData
                {
                    Message = "Erro ao realizar operação",
                    Errors = [notFounded.Message],
                    NotFounded = true
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new GenericResponseNoData
                {
                    Message = "Erro ao realizar operação",
                    Errors = ["Um erro interno ocorreu!"]
                });
            }
        }
    }
}
