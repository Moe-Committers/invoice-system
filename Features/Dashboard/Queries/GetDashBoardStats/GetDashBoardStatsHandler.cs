using invoice_system.Database;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStats, DashboardStatsDto>
{
    private readonly Db _db;

    public GetDashboardStatsQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<DashboardStatsDto> Handle(
        GetDashboardStats request,
        CancellationToken ct)
    {

        var today = DateTime.UtcNow.Date;

        var invoiceStats = await GetInvoiceStats(today, ct);

        var topCustomers = await GetTopCustomers(ct);

        var monthlyRevenue = await GetMonthlyRevenue(today, ct);

        var dashboardStats = new DashboardStatsDto
        {
            InvoiceStats = invoiceStats,
            TopCustomers = topCustomers,
            MonthlyRevenue = monthlyRevenue
        };

        return dashboardStats;
    }

    private async Task<InvoiceStats> GetInvoiceStats(DateTime today, CancellationToken ct)
    {
        var invoices = await _db.Invoices
            .Select(i => new
            {
                i.Status,
                i.FinalPayment,
                IsOverdue = i.ExpirationDate.HasValue && i.ExpirationDate.Value < today
            })
            .ToListAsync(ct);

        return new InvoiceStats
        {
            TotalInvoices = invoices.Count(),
            PaidInvoices = invoices.Count(i => i.Status.ToLower() == "paid"),
            UnpaidInvoices = invoices.Count(i => i.Status.ToLower() != "paid"),
            OverdueInvoices = invoices.Count(i => i.IsOverdue),
            TotalRevenue = invoices
                .Where(i => i.Status.ToLower() == "paid")
                .Sum(i => i.FinalPayment),
            OutstandingAmount = invoices
                .Where(i => i.Status.ToLower() != "paid")
                .Sum(i => i.FinalPayment)
        };
    }

    private async Task<List<TopCustomer>> GetTopCustomers(CancellationToken ct)
    {
        return await _db.Customers
            .Select(c => new TopCustomer
            {
                Id = c.Id,
                Name = c.Name,
                InvoiceCount = c.Invoices.Count(),
                TotalAmount = c.Invoices
                    .Sum(i => i.FinalPayment)
            })
            .OrderByDescending(c => c.InvoiceCount)
            .ThenByDescending(c => c.TotalAmount)
            .Take(5)
            .ToListAsync(ct);
    }

    private async Task<List<MonthlyRevenue>> GetMonthlyRevenue(DateTime today, CancellationToken ct)
    {
        var sixMonthsAgo = today.AddMonths(-5).Date;

        var monthlyData = await _db.Invoices
            .Where(i =>
                i.CreatedAt >= sixMonthsAgo &&
                i.Status.ToLower() == "paid")
            .GroupBy(i => new
            {
                Year = i.CreatedAt.Year,
                Month = i.CreatedAt.Month
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Amount = g.Sum(i => i.FinalPayment),
                Count = g.Count()
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToListAsync(ct);

        return monthlyData.Select(m => new MonthlyRevenue
        {
            Month = $"{m.Year}-{m.Month:D2}",
            Amount = m.Amount,
            InvoiceCount = m.Count
        }).ToList();
    }
}