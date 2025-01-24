using MediatR;

namespace invoice_system.Features.HelloWorld;

public class HellowQueryHandler : IRequestHandler<HellowQuery , string>{
    public Task<string> Handle(HellowQuery req , CancellationToken ct){
        return Task.FromResult(req.message);
    }
}