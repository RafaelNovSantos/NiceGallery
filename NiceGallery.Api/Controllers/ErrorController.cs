using Microsoft.AspNetCore.Mvc;

namespace NiceGallery.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult HandleError() =>
            Problem(title: "Erro inesperado", statusCode: 500);
    }

}
