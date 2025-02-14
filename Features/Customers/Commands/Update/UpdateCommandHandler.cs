using invoice_system.Database;
using invoice_system.Features.Customers.Commands.Create;
using invoice_system.Utils.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Customers.Commands.Update;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCommand, ActionCustomer>
{
    private readonly Db _db;

    public UpdateCustomerCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<ActionCustomer> Handle(UpdateCommand req, CancellationToken ct)
    {
        var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == req.CustomerId, ct);

        if (customer == null)
        {
            throw new NotFoundExceptions("Customer not found");
        }

        customer.Name = req.Name ?? customer.Name;
        customer.Attention = req.Attention ?? customer.Attention;
        customer.Tel = req.Tel ?? customer.Tel;

        _db.Customers.Update(customer);
        await _db.SaveChangesAsync(ct);

        return customer.Adapt<ActionCustomer>();
    }
}