
using Microsoft.AspNetCore.Identity;
using music_api.Model;

namespace music_api.Controllers;


[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class UserController : ControllerBase
{
    [HttpPost("CreateAccount")]
    public async Task<ActionResult> RegisterUser (
        [FromBody] User data,
        [FromServices] UserRepository repository
    ) 
    {
        User peopleIfExists = repository
            .FirstOrDefaultAsync( x => 
                x.Name  == data.Name ||
                x.Email == data.Email
        );

        if ( peopleIfExists != null )
        {
            if( peopleIfExists.Name == data.Name )
                return BadRequest("This username already exists");
            if( peopleIfExists.Email == data.Email )
                return BadRequest("This email already exists");
        }

        
    }
}