using GymTracer.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GymTracer.Extensions
{
    public static class ControllerExtension
    {
        public static IActionResult Run(this ControllerBase controllerBase, Func<IActionResult> function)
        {
            try
            {
                return function();
            }
            catch (ApiException ex)
            {
                return controllerBase.StatusCode(ex.StatusCode, new { 
                    error = ex.Message,
#if DEBUG
                    stackTrace = ex.StackTrace,
                    debugMessage = ex.Message
#endif
                });
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { 
                    error = "Invalid data",
#if DEBUG
                    stackTrace = ex.StackTrace,
                    debugMessage = ex.Message
#endif
                });
            }
        }
    }
}
