using invoice_system.Features.Dashboard.Queries.GetDashboardStats;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<ApiResponse<DashboardStatsDto>>> GetDashboardStats(GetDashboardStats query)
    {
        var result = await _mediator.Send(query);
        return Ok(ResHelper.Success(result));
    }
}