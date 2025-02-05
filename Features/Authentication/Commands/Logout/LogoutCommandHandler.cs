using invoice_system.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Authentication.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand , bool> {
    private readonly Db _db;

    public LogoutCommandHandler(Db db){
        _db = db;
    }

    public async Task<bool> Handle(LogoutCommand request , CancellationToken ct){
        var token = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == request.UserId && rt.Token == request.RefreshToken ,ct);
        if(token != null){
            _db.RefreshTokens.Remove(token);
            await _db.SaveChangesAsync(ct);
            return true;
        }
        return false;
    }
}