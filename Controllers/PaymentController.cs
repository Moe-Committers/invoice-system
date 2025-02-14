using invoice_system.Features.Payment.Queries.GetCustomerInvoices;
using invoice_system.Features.Payment.Queries.GetPayments;
using invoice_system.Models;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CustomerWithInvoiceCount>>>> GetPayments(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null)
    {
        var query = new GetPaymentsQuery
        {
            Page = page,
            PageSize = pageSize,
            Search = search
        };

        var result = await _mediator.Send(query);
        return result.success == true ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{customerId}/invoices")]
    public async Task<ActionResult<ApiResponse<Invoices>>> GetCustomerInvoices(long customerId, GetCustomerInvoices request)
    {
        var command = request with { CustomerId = customerId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}