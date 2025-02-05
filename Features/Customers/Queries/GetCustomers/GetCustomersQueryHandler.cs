using invoice_system.Database;
using invoice_system.Utils.DTOs.Customer;
using invoice_system.Utils.Extensions;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ApiResponse<List<CustomerDto>>>
{
    private readonly Db _db;

    public GetCustomersQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<ApiResponse<List<CustomerDto>>> Handle(GetCustomersQuery req, CancellationToken ct)
    {
        var query = _db.Customers.Include(c => c.Users).Select(c => new CustomerDto {
            Name = c.Name,
            Attention = c.Attention,
            Tel = c.Tel,
            CreatedAt = c.CreatedAt,
            UserName = c.Users.Name
        }).AsQueryable();

        if (!string.IsNullOrEmpty(req.Search))
        {
            query = query.Where(c => c.Name.Contains(req.Search) || c.Attention.Contains(req.Search) || c.Tel.Contains(req.Search));
        }

        if (req.FromDate.HasValue)
        {
            query = query.Where(c => c.CreatedAt >= req.FromDate);
        }

        if (req.ToDate.HasValue)
        {
            query = query.Where(c => c.CreatedAt <= req.ToDate);
        }

        query = req.Sort?.ToLower() switch
        {
            "name" => req.IsAscending
                ? query.OrderBy(c => c.Name)
                : query.OrderByDescending(c => c.Name),
            "created" => req.IsAscending
                ? query.OrderBy(c => c.CreatedAt)
                : query.OrderByDescending(c => c.CreatedAt),
            _ => query.OrderByDescending(c => c.CreatedAt)
        };

        return await query.UsePaginate<CustomerDto, Models.Customers>(req.Page, req.PageSize, ct);
    }
}