
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
        [FromBody] SigninData data,
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

        User newUserData = new() {
            Name = data.Name,
            Email = data.Email,
            Password = data.Password,
            Birth = data.Birth,
            Salt = PasswordConfig.GenerateStringSalt(12)
        };

        newUserData.Password = PasswordConfig.GetHash(
            newUserData.Password,
            newUserData.Salt
        );

        try
        {
            await repository.add(newUserData);
            return Ok("Subscription successfull");
        }
        catch (System.Exception exp)
        {
            return BadRequest($"{exp}");
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult> LoginUser (
        [FromBody] LoginData data,
        [FromServices] IRepository<User> repository,
        [FromServices] IJwtService jwt
    )
    {
        System.Console.WriteLine($"UserData: {data.Identify},{data.Password}");

        var user = await repository.FirstOrDefaultAsync( user => 
            user.Email == data.Identify ||
            user.Name.Contains(data.Identify)
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

    [HttpPost("DeleteUser")]
    public async Task<ActionResult> DeleteUser (
        [FromBody]JWT data,
        [FromServices] IRepository<User> repository,
        [FromServices] IJwtService jwt
    )
    {

        var jwtResult = jwt.Validate<ReturnLoginData>(data.Value);

        var user = await repository.FirstOrDefaultAsync( u => 
            u.Name == jwtResult.Name  || 
            u.Email == jwtResult.Email
        );
        try {
            await repository.Delete(user);
            return Ok("User deleted");
        }
        catch (System.Exception exp) {
            return BadRequest(exp);
        }
    }
}