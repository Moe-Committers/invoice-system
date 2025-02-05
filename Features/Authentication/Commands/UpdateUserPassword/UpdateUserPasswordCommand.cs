
using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.UpdateUserPassword;

public record UpdateUserPassCommand : IRequest<bool> {
    [JsonIgnore]
    public long Id {get; set;}
    public string CurrentPassword {get; set;}
    public string NewPassword {get; set;}
    public string ConfirmPassword {get; set;}
};