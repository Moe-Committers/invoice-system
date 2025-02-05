using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Customers.Queries.GetCustomer;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Models.Customers>
{
    private readonly Db _db;

    public GetCustomerQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Customers> Handle(GetCustomerQuery req, CancellationToken ct)
    {
        var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == req.CustomerId, ct);

        if (customer == null)
        {
            throw new NotFoundExceptions("Customer not found");
        }

        return customer;
    }
}