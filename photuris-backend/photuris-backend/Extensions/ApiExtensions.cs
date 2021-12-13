using Microsoft.AspNetCore.Mvc;

namespace photuris_backend.Extensions
{
    public static class ApiExtensions
    {
        public static IActionResult InternalServerError(this ControllerBase controller, string? message = null)
        {
            return controller.StatusCode(StatusCodes.Status500InternalServerError, message);
        }
    }
}
