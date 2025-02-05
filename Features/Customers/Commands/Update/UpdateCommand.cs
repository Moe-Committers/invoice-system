using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Customers.Commands.Update;

public record UpdateCommand : IRequest<Models.Customers>
{
    [JsonIgnore]
    public long CustomerId { get; set; }
    public string Name { get; set; }
    public string Attention { get; set; }
    public string Tel { get; set; }
}