using invoice_system.Database;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Authentication.Commands.UpdateUserPassword;

public class UpdateUserPassCommandHandler : IRequestHandler<UpdateUserPassCommand, UserDto>
{
    private readonly Db _db;
    public UpdateUserPassCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<UserDto> Handle(UpdateUserPassCommand request, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
        if (user == null)
        {
            throw new NotFoundExceptions("User not founded!");
        }
        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
        {
            throw new InvalidCurrentPassword("Your old Password is not correct");
        }
        if (request.NewPassword != request.ConfirmPassword)
        {
            throw new InvalidConfirmPassword("Invalid Confirm password!");
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _db.SaveChangesAsync();
        var userDto = user.Adapt<UserDto>();
        userDto.Role = user.Role.Name;
        return userDto;
    }
}