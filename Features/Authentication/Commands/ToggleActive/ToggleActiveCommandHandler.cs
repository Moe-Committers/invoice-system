using invoice_system.Database;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Enums;
using invoice_system.Utils.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Authentication.Commands.ToggleActive;

public class ToggleActiveCommandHandler : IRequestHandler<ToggleActiveCommand, UserDto>
{
    private readonly Db _db;

    public ToggleActiveCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<UserDto> Handle(ToggleActiveCommand request, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
        if (user == null)
        {
            throw new NotFoundExceptions("the user is actually not founded");
        }
        user.Status = user.Status == Status.isActive ? Status.inActive : Status.isActive;
        await _db.SaveChangesAsync(ct);
        return user.Adapt<UserDto>();
    }
}