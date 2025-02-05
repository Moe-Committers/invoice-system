using MediatR;

namespace invoice_system.Features.Customers.Queries.GetCustomer;

public class GetCustomerQuery : IRequest<Models.Customers>
{
    public long CustomerId { get; set; }
}