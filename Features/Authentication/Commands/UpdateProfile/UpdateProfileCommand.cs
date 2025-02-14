using System.Text.Json.Serialization;
using invoice_system.Utils.DTOs.Authentication;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.UpdateProfile;

public record UpdateProfileCommand : IRequest<UserDto>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public IFormFile? Img { get; set; }
};

public class UpdateProfileForm
{
    public string? Name { get; set; }
    public IFormFile? Img { get; set; }
}