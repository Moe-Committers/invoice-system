using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase{
    [HttpPost("login")]
    public async Task<ActionResult> Login (){
        return Ok();
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register (){
        return Ok();
    }
    [HttpGet("user")]
    public async Task<ActionResult> User (){
        return Ok();
    }
    [HttpPost("logout")]
    public async Task<ActionResult> Logout (){
        return Ok();
    }
}