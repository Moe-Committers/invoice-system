using MediatR;

namespace invoice_system.Features.HelloWorld;

public record HellowQuery(string message) : IRequest<string>;