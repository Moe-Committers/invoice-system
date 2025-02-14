using System.Text.Json.Serialization;
using invoice_system.Utils.DTOs.Authentication;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.ToggleActive;

public record ToggleActiveCommand : IRequest<UserDto>
{
    [JsonIgnore]
    public long Id { get; set; }
    public bool Toggle { get; set; }
};

public class ToggleActive
{
    public bool Toggle { get; set; }
}