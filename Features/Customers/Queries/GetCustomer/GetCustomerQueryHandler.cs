using invoice_system.Database;
using invoice_system.Utils.DTOs.Customer;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Customers.Queries.GetCustomer;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly Db _db;

    public GetCustomerQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<CustomerDto> Handle(GetCustomerQuery req, CancellationToken ct)
    {
        var customer = await _db.Customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Attention = c.Attention,
            Tel = c.Tel,
            UserName = c.Users.Name,
            CreatedAt = c.CreatedAt
        }).FirstOrDefaultAsync(c => c.Id == req.CustomerId, ct);

        if (customer == null)
        {
            throw new NotFoundExceptions("Customer not found");
        }

        return customer;
    }
}