using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Customers.Commands.Delete;

public record DeleteCommand : IRequest<bool>
{
    [JsonIgnore]
    public long CustomerId { get; set; }
}