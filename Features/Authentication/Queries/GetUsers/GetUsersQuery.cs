using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Authentication.Queries.GetUsers;

public record GetUsersQuery : IRequest<ApiResponse<List<UserDto>>> {
    public string? Search {get; set;}
    public long? Id {get; set;}
    public DateTime? FromDate {get; set;}
    public DateTime? ToDate {get; set;}
    public string? sort {get; set;} = "created";
    public bool IsAscending {get; set;} = false;
    public int Page {get; init; } = 1;
    public int PageSize {get; init;} = 10;
}