using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Customers.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Models.Customers>
{
    private readonly Db _db;
    public CreateCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Customers> Handle(CreateCommand req, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == req.UserId , ct);

        if (user == null)
        {
            throw new NotFoundExceptions("user not found");
        }

        var newCustomer = new Models.Customers
        {
            Name = req.Name,
            Attention = req.Attention,
            Tel = req.Tel,
            UserId = user.Id
        };

        await _db.Customers.AddAsync(newCustomer, ct);

        await _db.SaveChangesAsync(ct);

        return newCustomer;
    }
}