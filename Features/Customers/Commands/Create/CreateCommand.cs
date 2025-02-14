using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Customers.Commands.Create;

public record CreateCommand : IRequest<ActionCustomer>
{
    public string Name { get; set; }
    public string Attention { get; set; }
    public string Tel { get; set; }
    [JsonIgnore]
    public long UserId { get; set; }
}

public class ActionCustomer
{
    public string Name { get; set; }
    public string Attention { get; set; }
    public string Tel { get; set; }
    public long UserId { get; set; }
}