using System.ComponentModel.DataAnnotations.Schema;
using invoice_system.Utils.DTOs.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace invoice_system.Features.Authentication.Commands.UpdateProfile;

public record UpdateProfileCommand : IRequest<UserDto> {
    public long Id {get; set;}
    public string? Name {get; set;}
    public IFormFile? Img {get; set;}
};