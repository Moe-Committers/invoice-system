using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Dashboard")]
public class DashboardController : ControllerBase{
    [HttpGet]
    public async Task<ActionResult> getDash (){
        return Ok();
    }
}