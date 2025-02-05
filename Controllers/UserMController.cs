using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/UserM")]
public class UserMController : ControllerBase{
    [HttpPut("role/permissions")]
    public async Task<ActionResult> RolePermissions (){
        return Ok();
    }
    [HttpPut("user/role/{id}")]
    public async Task<ActionResult> UserRole (){
        return Ok();
    }
}