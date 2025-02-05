using MediatR;

namespace invoice_system.Features.Customers.Commands.Create;

public record CreateCommand : IRequest<Models.Customers>{
    public string Name {get; set;}
    public string Attention {get; set;}
    public string Tel {get; set;}
    public long UserId {get; set;}
}