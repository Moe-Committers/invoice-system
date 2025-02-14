
using System.Text.Json.Serialization;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.UpdateUserPassword;

public record UpdateUserPassCommand : IRequest<UserDto> {
    [JsonIgnore]
    public long Id {get; set;}
    public string CurrentPassword {get; set;}
    public string NewPassword {get; set;}
    public string ConfirmPassword {get; set;}
};