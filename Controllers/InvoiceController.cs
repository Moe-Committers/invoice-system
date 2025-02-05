using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Invoice")]
public class InvoiceController : ControllerBase{
    [HttpPost]
    public async Task<ActionResult> CreateInvoice (){
        return Ok();
    }
    [HttpGet]
    public async Task<ActionResult> GetInvoices (){
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetInvoice (){
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateInvoice (){
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInvoice (){
        return Ok();
    }
}