using invoice_system.Database;
using invoice_system.Utils.DTOs.QuotationDto;
using invoice_system.Utils.Extensions;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Quotation.Queries.GetQuotations;

public class GetQuotationsQueryHandler : IRequestHandler<GetQuotationsQuery, ApiResponse<List<QtDto>>>
{
    private readonly Db _db;

    public GetQuotationsQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<ApiResponse<List<QtDto>>> Handle(GetQuotationsQuery request, CancellationToken ct)
    {
        var query = _db.Quotations
            .Include(q => q.Customer)
            .Include(q => q.Users)
            .Select(q => new QtDto
            {
                Id = q.Id,
                QuotationNumber = q.QuotationNumber,
                Status = q.Status,
                CustomerName = q.Customer.Name,
                CreatorName = q.Users.Name,
                FinalPayment = q.FinalAmount,
                Date = q.Date
            })
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(q => q.CustomerName.Contains(request.UserName));
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            query = query.Where(q => q.Status == request.Status);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(q => q.Date >= request.StartDate);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(q => q.Date <= request.EndDate);
        }

        query = request.Sort?.ToLower() switch
        {
            "invno" => request.IsAscending
                ? query.OrderBy(c => c.QuotationNumber)
                : query.OrderByDescending(c => c.QuotationNumber),
            "created" => request.IsAscending
                ? query.OrderBy(c => c.Date)
                : query.OrderByDescending(c => c.Date),
            _ => query.OrderByDescending(c => c.Date)
        };

        return await query.UsePaginate(request.Page, request.PageSize, ct);
    }
}