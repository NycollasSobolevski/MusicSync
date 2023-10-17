
using Microsoft.AspNetCore.Identity;
using music_api.Auxi;
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
        [FromServices] IRepository<User> repository,
        [FromServices] IRepository<Token> tokenRepository
    ) 
    {

        User peopleExists = await repository
            .FirstOrDefaultAsync( x => 
                x.Name == data.Name ||
                x.Email == data.Email
        );

        if ( peopleExists != null )
        {
            if( peopleExists.Name == data.Name )
                return BadRequest("This username already exists");
            if( peopleExists.Email == data.Email )
                return BadRequest("This email already exists");
        }

        User newUserData = new() {
            Name = data.Name,
            Email = data.Email,
            Password = data.Password,
            Birth = data.Birth,
            Salt = PasswordConfig.GenerateStringSalt(12),
            EmailConfirmed = false,
            IsActive = true
        };

        newUserData.Password = PasswordConfig.GetHash(
            newUserData.Password,
            newUserData.Salt
        );

        try
        {
            await repository.add(newUserData);

            var token = await tokenRepository.add(new Token() {
                User = newUserData.Name,
                Service = "Email",
                ServiceToken = Rand.GetRandomString(6),
            });
            
            SendEmail.SendEmailValidation(newUserData.Email,newUserData.Name,token.ServiceToken );

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
        // System.Console.WriteLine($"UserData: {data.Identify},{data.Password}");

        var user = await repository.FirstOrDefaultAsync( user => 
            user.Email == data.Identify ||
            user.Name.Contains(data.Identify)
        );

        if(user == null || !user.IsActive)
            return NotFound("Username or Email not exists");

        if ( PasswordConfig.GetHash(data.Password, user.Salt) != user.Password )
            return Unauthorized("Username or email incorrects");

        UserJwtData returnUser = new UserJwtData{
            Name  = user.Name,
            Email = user.Email
        };


        var returnJwt = new JWT(){
            Value = jwt.GetToken(returnUser)
        };

        return Ok(new JWTWithData<bool>() { Jwt = returnJwt, Data = user.EmailConfirmed });
    }

    [HttpPost("DeleteUser")]
    public async Task<ActionResult> DeleteUser (
        [FromBody]JWT data,
        [FromServices] IRepository<User> repository,
        [FromServices] IJwtService jwt
    )
    {

        var jwtResult = jwt.Validate<UserJwtData>(data.Value);

        var user = await repository.FirstOrDefaultAsync( u => 
            u.Name == jwtResult.Name  || 
            u.Email == jwtResult.Email
        );
        user.IsActive = false;
        
        try {
            await repository.Update(user);
            return Ok("User deleted");
        }
        catch (System.Exception exp) {
            return BadRequest(exp);
        }
    }

    [HttpPost("VerifyEmail")]
    public async Task<ActionResult> VerifyEmail (
        [FromBody]JWTWithData<string> data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IJwtService jwt
    ){
        try{
            var jwtUser = jwt.Validate<UserJwtData>(data.Jwt.Value);
            var user = await userRepository.FirstOrDefaultAsync( user => 
                user.Name == jwtUser.Name &&
                user.Email == jwtUser.Email
            );
            
            var token = await tokenRepository.FirstOrDefaultAsync( t => 
                t.User == user.Name &&
                t.Service == "Email"
            );

            if (token.ServiceToken.ToLower() != data.Data.ToLower())
                return Unauthorized("Invelid Token");
            
            user.EmailConfirmed = true;
            await userRepository.Update(user);

            return Ok("Token Validated");
        }
        catch (System.Exception exp) {
            return BadRequest(exp);
        };
    }
}