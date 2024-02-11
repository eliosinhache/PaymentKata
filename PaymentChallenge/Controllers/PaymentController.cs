using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentChallenge.Business;
using PaymentChallenge.Contract.DB;
using PaymentChallenge.Contract.ExternalServices;
using PaymentChallenge.Data;
using PaymentChallenge.Model.Request;

namespace PaymentChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentLogic _paymentService;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentLogic paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewPayment paymentRequest)
        {
            var newPayment = _mapper.Map<Payment>(paymentRequest);
            var result = _paymentService.NewPayment(newPayment);
            return result.Success ? Ok(result.Payment) : BadRequest(result.Error);
        }


        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> Confirm(ConfirmPayment confirmOp)
        {
            var updatePayment = _mapper.Map<Payment>(confirmOp);
            var result = _paymentService.ConfirmPayment(updatePayment);
            return result.Success ? Ok(result.Payment) : BadRequest(result.Error);
        }
    }
}
