using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Dashboard.Queries.GetDashboardStats;

public record GetDashboardStats : IRequest<DashboardStatsDto>;

public class DashboardStatsDto
{
    public InvoiceStats InvoiceStats { get; set; }
    public List<TopCustomer> TopCustomers { get; set; }
    public List<MonthlyRevenue> MonthlyRevenue { get; set; }
}

public class InvoiceStats
{
    public int TotalInvoices { get; set; }
    public int PaidInvoices { get; set; }
    public int UnpaidInvoices { get; set; }
    public int OverdueInvoices { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal OutstandingAmount { get; set; }
}

public class TopCustomer
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int InvoiceCount { get; set; }
    public decimal TotalAmount { get; set; }
}

public class MonthlyRevenue
{
    public string Month { get; set; }
    public decimal Amount { get; set; }
    public int InvoiceCount { get; set; }
}
