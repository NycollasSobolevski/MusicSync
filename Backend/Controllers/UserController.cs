
using Microsoft.AspNetCore.Identity;
using music_api.DTO;
using music_api.Model;
using music_api.Password;
using Security_jwt;

namespace music_api.Controllers;


[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class UserController : ControllerBase
{
    [HttpPost("CreateAccount")]
    public async Task<ActionResult> RegisterUser (
        [FromBody] User data,
        [FromServices] IRepository<User> repository
    ) 
    {
        User peopleIfExists = await repository
            .FirstOrDefaultAsync( x => 
                x.Name == data.Name ||
                x.Email == data.Email
        );

        if ( peopleIfExists != null )
        {
            if( peopleIfExists.Name == data.Name )
                return BadRequest("This username already exists");
            if( peopleIfExists.Email == data.Email )
                return BadRequest("This email already exists");
        }

        data.Salt = PasswordConfig.GenerateStringSalt(12);
        data.Password = PasswordConfig.GetHash(
            data.Password,
            data.Salt
        );

        try
        {
            await repository.add(data);
            return Ok("Subscription successfull");
        }
        catch (System.Exception exp)
        {
            return BadRequest($"{exp}");
        }
    }

    [HttpGet("Login")]
    public async Task<ActionResult> LoginUser (
        [FromBody] LoginData data,
        [FromServices] IRepository<User> repository,
        [FromServices] IJwtService jwt
    )
    {
        var user = await repository.FirstOrDefaultAsync( user => 
            user.Email == data.Identitify ||
            user.Name.Contains(data.Identitify)
        );

        if(user == null)
            return NotFound("Username or Email not exists");

        if ( PasswordConfig.GetHash(data.Password, user.Salt) != user.Password )
            return Unauthorized("Username or email incorrects");

        ReturnLoginData returnUser = new ReturnLoginData{
            Name  = user.Name,
            Email = user.Email
        };

        string returnJwt = jwt.GetToken(returnUser);

        return Ok(new JWT() { Value = returnJwt });
    }
}