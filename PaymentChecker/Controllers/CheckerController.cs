using Microsoft.AspNetCore.Mvc;

namespace PaymentChecker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(float payment)
        {
            return payment % 1 == 0? Ok("Pago autorizado") : BadRequest("Pago rechazado");
        }
    }
}
