using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Payment")]
public class PaymentController : ControllerBase{
    [HttpGet]
    public async Task<ActionResult> Payments (){
        return Ok();
    }
    [HttpGet("{customerId}")]
    public async Task<ActionResult> GetCustomerPayment (){
        return Ok();
    }
}