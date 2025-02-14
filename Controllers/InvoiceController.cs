using System.Security.Claims;
using invoice_system.Features.Invoice.Commands.Create;
using invoice_system.Features.Invoice.Commands.Delete;
using invoice_system.Features.Invoice.Commands.Update;
using invoice_system.Features.Invoice.Queries.GetInvoice;
using invoice_system.Features.Invoice.Queries.GetInvoices;
using invoice_system.Models;
using invoice_system.Utils.DTOs.invoiceDto;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<InvDto>>> GetInvoices([FromQuery] GetInvoicesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Invoices>>> GetInvoice(long id)
    {
        var query = new GetInvoiceQuery { InvoiceId = id };
        var result = await _mediator.Send(query);
        return Ok(ResHelper.Success(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Invoices>>> CreateInvoice(CreateInvoiceCommand request)
    {
        var user = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var command = request with { UserId = user };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<Invoices>>> UpdateInvoice(long id, UpdateInvoiceCommand request)
    {
        var command = request with { InvoiceId = id };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteInvoice(long id)
    {
        var command = new DeleteInvoiceCommand { InvoiceId = id };
        var result = await _mediator.Send(command);
        return result ? Ok(ResHelper.Success<ActionResult>()) : BadRequest(ResHelper.Errors<ActionResult>());
    }
}