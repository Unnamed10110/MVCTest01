using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MVCTest01.Controllers
{
    public class ErrorController : Controller
    {

        private readonly ILogger<ErrorController> logs;

        public ErrorController(ILogger<ErrorController> log)
        {
            this.logs = log;
        }


        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "El recurso solicitado no se puede encontrar";
                    break;
            }
            return View("Error");
        }


        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            logs.LogError($"Ruta del ERROR: {exceptionHandlerPathFeature.Path}" +
            $"Excepcion: {exceptionHandlerPathFeature. Error}" +
            $"Traza del ERROR: {exceptionHandlerPathFeature.Error.StackTrace}");
            return View("ErrorGenerico");
        }
    }
}
