using invoice_system.Database;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Exceptions;
using invoice_system.Utils.Helpers.ImageHelpers;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Authentication.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UserDto>
{
    private readonly Db _db;
    private readonly IImageService _img;
    public UpdateProfileCommandHandler(Db db, IImageService img)
    {
        _db = db;
        _img = img;
    }

    public async Task<UserDto> Handle(UpdateProfileCommand request, CancellationToken ct)
    {
        var user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == request.Id);
        if (user == null)
        {
            throw new NotFoundExceptions("User not founded!");
        }
        user.Name = request.Name ?? user.Name;
        if (request.Img != null)
        {
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                _img.DeleteImage(user.Avatar);
            }
            user.Avatar = await _img.UploadImage(request.Img, "Profile-pic");
        }
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);
        var userDto = user.Adapt<UserDto>();
        userDto.Role = user.Role.Name;
        return userDto;
    }
}