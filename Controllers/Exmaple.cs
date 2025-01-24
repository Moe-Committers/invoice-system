using invoice_system.Features.HelloWorld;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Api/Example")]
public class Example : ControllerBase{
    private readonly IMediator _mediatr;
    public Example(IMediator mediatr){
        _mediatr = mediatr;
    }
    [HttpGet]
    public ActionResult<string> HelloWorld(HellowQuery command){
        var response = _mediatr.Send(command);
        return Ok(ResHelper.Success(response));
    }
}