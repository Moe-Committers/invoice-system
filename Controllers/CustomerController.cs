using System.Security.Claims;
using invoice_system.Features.Customers.Commands.Create;
using invoice_system.Features.Customers.Commands.Delete;
using invoice_system.Features.Customers.Commands.Update;
using invoice_system.Features.Customers.Queries.GetCustomer;
using invoice_system.Features.Customers.Queries.GetCustomers;
using invoice_system.Models;
using invoice_system.Utils.DTOs.Customer;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult<Customers>> CreateCustomer(CreateCommand request)
    {
        var user = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var command = request with { UserId = user };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }
    [HttpGet]
    public async Task<ActionResult<ApiResponse<CustomerDto>>> GetCustomers([FromQuery] GetCustomersQuery command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Customers>> GetCustomer(long id)
    {
        var command = new GetCustomerQuery { CustomerId = id };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Customers>> UpdateCustomer(long id, UpdateCommand request)
    {
        var command = request with { CustomerId = id };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCustomer(long id)
    {
        var command = new DeleteCommand { CustomerId = id };
        var result = await _mediator.Send(command);
        return result ? Ok(ResHelper.Success<ActionResult>()) : BadRequest(ResHelper.Errors<ActionResult>());
    }
}