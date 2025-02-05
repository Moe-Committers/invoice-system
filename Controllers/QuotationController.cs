using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Quotation")]
public class QutationController : ControllerBase{
    [HttpPost]
    public async Task<ActionResult> CreateQuotation (){
        return Ok();
    }
    [HttpGet]
    public async Task<ActionResult> GetQuotations (){
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetQuotation (){
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateQuotation (){
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuotation (){
        return Ok();
    }
    [HttpPut("/convert-to-invoice")]
    public async Task<ActionResult> ConvertToInvoice(){
        return Ok();
    }
}