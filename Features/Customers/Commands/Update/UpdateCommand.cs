using System.Text.Json.Serialization;
using invoice_system.Features.Customers.Commands.Create;
using MediatR;

namespace invoice_system.Features.Customers.Commands.Update;

public record UpdateCommand : IRequest<ActionCustomer>
{
    [JsonIgnore]
    public long CustomerId { get; set; }
    public string Name { get; set; }
    public string Attention { get; set; }
    public string Tel { get; set; }
}