using System.Security.Claims;
using invoice_system.Features.Quotation.Commands.Convert;
using invoice_system.Features.Quotation.Commands.Create;
using invoice_system.Features.Quotation.Commands.Delete;
using invoice_system.Features.Quotation.Commands.Update;
using invoice_system.Features.Quotation.Queries.GetQuotation;
using invoice_system.Features.Quotation.Queries.GetQuotations;
using invoice_system.Models;
using invoice_system.Utils.DTOs.QuotationDto;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class QuotationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuotationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<QtDto>>> GetQuotations([FromQuery] GetQuotationsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Quotations>>> GetQuotation(long id)
    {
        var query = new GetQuotationQuery { QuotationId = id };
        var result = await _mediator.Send(query);
        return Ok(ResHelper.Success(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Quotations>>> CreateQuotation(CreateQuotationCommand request)
    {
        var user = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var command = request with { UserId = user };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<Quotations>>> UpdateQuotation(
        long id,
        UpdateQuotationCommand request)
    {
        var command = request with { QuotationId = id };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteQuotation(
        long id)
    {
        var command = new DeleteQuotationCommand { QuotationId = id };
        var result = await _mediator.Send(command);
        return result ? Ok(ResHelper.Success<ActionResult>()) : BadRequest(ResHelper.Errors<ActionResult>());
    }

    [HttpPost("{id}/convert")]
    public async Task<ActionResult<ApiResponse<Invoices>>> ConvertToInvoice(
        long id)
    {
        var command = new ConvertQuotationCommand { QuotationId = id };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }
}