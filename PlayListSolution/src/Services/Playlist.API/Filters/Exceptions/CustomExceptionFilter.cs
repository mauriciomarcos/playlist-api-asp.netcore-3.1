using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Playlist.API.Filters.Exceptions
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var returnDefaultCustonException = new ObjectResult(new
            {
                MensagemErro = context.Exception.Message,
                StakTraceErro = context.Exception.StackTrace
            });
            returnDefaultCustonException.StatusCode = 500;

            context.Result = returnDefaultCustonException;            
        }
    }
}