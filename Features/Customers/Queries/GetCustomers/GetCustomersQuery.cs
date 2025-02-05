using invoice_system.Utils.DTOs.Customer;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<ApiResponse<List<CustomerDto>>>
{
    public string? Search { get; init; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Sort { get; set; } = "created";
    public bool IsAscending { get; set; } = false;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}