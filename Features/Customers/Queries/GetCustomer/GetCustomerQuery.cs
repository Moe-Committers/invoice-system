using invoice_system.Utils.DTOs.Customer;
using MediatR;

namespace invoice_system.Features.Customers.Queries.GetCustomer;

public class GetCustomerQuery : IRequest<CustomerDto>
{
    public long CustomerId { get; set; }
}