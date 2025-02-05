using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Customer")]
public class CustomerController : ControllerBase{
    [HttpPost]
    public async Task<ActionResult> CreateCustomer (){
        return Ok();
    }
    [HttpGet]
    public async Task<ActionResult> GetCustomers (){
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetCustomer (){
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer (){
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer (){
        return Ok();
    }
    [HttpGet("dropdown")]
    public async Task<ActionResult> DropdownCustomer (){
        return Ok();
    }
}