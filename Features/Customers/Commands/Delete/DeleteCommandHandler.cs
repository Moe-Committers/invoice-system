using invoice_system.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Customers.Commands.Delete;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCommand , bool>
    {
        private readonly Db _db;

        public DeleteCustomerCommandHandler(Db db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteCommand req, CancellationToken ct)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == req.CustomerId, ct);

            if (customer == null)
            {
                return false;
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }