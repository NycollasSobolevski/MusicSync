
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson.IO;
using music_api;
using music_api.Controllers;
using music_api.DTO;
using music_api.Model;
using SpotifyAPI.Web;
using ZstdSharp.Unsafe;

#pragma warning disable
[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class DeezerController : StreamerController
{

    // private readonly string serverPort = Environment.GetEnvironmentVariable("SERVER_PORT");
    // private readonly string frontPort = Environment.GetEnvironmentVariable("FRONTEND_PORT");
    private readonly string clientUrl = Environment.GetEnvironmentVariable("DEEZER_CLIENT_URL");

    private readonly string clientId = Environment.GetEnvironmentVariable("DEEZER_CLIENT_ID");
    private readonly string clientSecret = Environment.GetEnvironmentVariable("DEEZER_CLIENT_SECRET");
    private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("FRONTEND_PORT")}/Callback?streamer=deezer";

    [HttpPost("GetData")]
    public override async Task<ActionResult<StringReturn>> Get(
        [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IJwtService jwt
    )
    {

        try{
            var userJwt = jwt.Validate<UserJwtData>(data.Value);
            var user = await userRepository.FirstOrDefaultAsync( user => 
                user.Name == userJwt.Name
            );

            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == user.Name && token.Service == "Deezer"
            );
            System.Console.WriteLine(token);
            if(token != null){
                return Ok(new StringReturn{
                    Data = "http://localhost:4200/?tab=Deezer"
                });
            }

            string scope = """
                basic_access, 
                manage_library,
                offline_access,
                delete_library,
                manage_community
            """;

            string deezerUriConnect = 
                $"https://connect.deezer.com/oauth/auth.php?app_id={clientId}&redirect_uri={redirectCallback}&perms={scope}";
            
            return Ok( new StringReturn{
                Data = deezerUriConnect
            });
        } catch (Exception e){
            System.Console.WriteLine(e.Message);
            return BadRequest("Unknow server error");
        }

    }


    [HttpPost("Callback")]
    public override async Task<ActionResult> Callback([FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    ){
        try{
            System.Console.WriteLine(data.code);

            var userJwt = jwt.Validate<UserJwtData>(data.jwt);
            var user = await userRepository.FirstOrDefaultAsync( user => 
                user.Name == userJwt.Name
            );

            string url = $"https://connect.deezer.com/oauth/access_token.php?app_id={this.clientId}&secret={this.clientSecret}&code={data.code}&output=json";
            System.Console.WriteLine(url);
            var response = await client.GetAsync(url);

            if(response.StatusCode != HttpStatusCode.OK){
                return BadRequest("Error getting token");
            }
            var tokenData = await response.Content.ReadFromJsonAsync<DeezerToken>();
            System.Console.WriteLine(tokenData);
            Token _token = new() 
            {
                User = user.Name,
                Service = "Deezer",
                ServiceToken = tokenData.access_token,
                ExpiresIn = tokenData.expires,
                LastUpdate = DateTime.Now
            };
            System.Console.WriteLine($"Token: {_token.ServiceToken}\nRefreshToken: {_token.RefreshToken}\nExpiresIn: {_token.ExpiresIn}\nLastUpdate: {_token.LastUpdate}");
            await tokenRepository.add(_token);

            return Ok();

        } catch (Exception e){
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }


    [HttpPost("GetMoreTracks")]
    public override Task<ActionResult> GetMoreTracks([FromBody] JWTWithData<string> body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetMusicData")]
    public override Task GetMusicData([FromServices] HttpClient client, [FromBody] string accessToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetPlaylist/")]
    public override Task<ActionResult> GetPlaylist([FromQuery(Name = "id")] string id, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetPlaylistTracks/")]
    public override async Task<ActionResult> GetPlaylistTracks(
        [FromQuery(Name = "id")] string id, 
        [FromQuery(Name = "streamer")] string streamer, 
        [FromBody] JWT body, 
        [FromServices] IJwtService jwt, 
        [FromServices] IRepository<Token> tokenRepository, 
        [FromServices] HttpClient client,
        [FromServices] IRepository<User> userRepository
    ){
        var user = await UserTools.ValidateUser(userRepository, jwt, body.Value);
        if(user == null) return BadRequest("Invalid user");
        try
        {
            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == user.Name && token.Service == "Deezer"
            );
            if(token == null) return Unauthorized("Token not found");

            string url = $"https://api.deezer.com/playlist/{id}?access_token={token.ServiceToken}";
            var res = await client.GetAsync(url);
            var playlist = await res.Content.ReadFromJsonAsync<DeezerPlaylistsData>();
            string stt= await res.Content.ReadAsStringAsync();
            System.Console.WriteLine(stt);
            return Ok(playlist);
        }
        catch (System.Exception e)
        {   
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }

    [HttpPost("GetUserPlaylists")]
    public override async Task<ActionResult> GetUserPlaylists(
        [FromBody] JWTWithGetPlaylistData data, 
        [FromServices] IJwtService jwt, 
        [FromServices] IRepository<User> userRepository, 
        [FromServices] IRepository<Token> tokenRepository, 
        [FromServices] HttpClient client
    ){
        

        try{
            var _user = await UserTools.ValidateUser(userRepository, jwt, data.Jwt.Value);
            
            if(User == null) return BadRequest("Invalid user");
            
            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == _user.Name && token.Service == "Deezer"
            );
            if (token == null) return Unauthorized("Token not found");


            string url = $"https://api.deezer.com/user/me/playlists?access_token={token.ServiceToken}";
            var res = await client.GetAsync(url);
            var playlists = await res.Content.ReadFromJsonAsync<DeezerPlaylistsData>();

            PlaylistsData result = new PlaylistsData();
            result.Items = new List<PlaylistData>();
            result.Total = playlists.total;

            foreach (var item in playlists.data)
            {
                PlaylistData _playlist = new PlaylistData(){
                    Id = item.id.ToString(),
                    Name = item.title,
                    Description = "",
                    Owner = new PlaylistOwner{
                        id = item.creator.id.ToString(),
                        display_name = item.creator.name,
                    }
                };
                _playlist.Images = new List<Images>();
                _playlist.Images.Add(new Images{
                    url = item.picture_small,
                    height = 56,
                    width = 56
                });
                _playlist.Images.Add(new Images{
                    url = item.picture_medium,
                    height = 250,
                    width = 250
                });
                _playlist.Images.Add(new Images{
                    url = item.picture_big,
                    height = 500,
                    width = 500
                });
                _playlist.Images.Add(new Images{
                    url = item.picture_xl,
                    height = 1000,
                    width = 1000
                });

                result.Items.Add(_playlist);
                
                
            }
            
            return Ok(result);

        } catch (Exception e){
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }

    [HttpPost("logoff")]
    public override async Task<ActionResult> LogOff([FromBody] JWT data, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository)
    {
        try
        {
            var _user = await UserTools.ValidateUser(userRepository, jwt, data.Value);
            if(_user == null) return BadRequest("Invalid user");

            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == _user.Name && token.Service == "Deezer"
            );
            
            if(token == null) return BadRequest("Token not found");

            await tokenRepository.Delete(token);
            return Ok();
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }

    [HttpPost("RefreshToken")]
    public override Task<ActionResult> RefreshToken([FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwtService, [FromBody] JWT jwt)
    {
        throw new NotImplementedException();
    }

    protected override async Task<DeezerUserData> GetUserData([FromServices] HttpClient client, string token)
    {
        try{
            string url = $"https://api.deezer.com/v1/me?access_token={token}";
            var res = await client.GetAsync(url);
            DeezerUserData userdata = await res.Content.ReadFromJsonAsync<DeezerUserData>();
            return userdata;

        } catch (Exception e) {
            System.Console.WriteLine(e.Message);
            return null;
        }

    }

    protected override Task refreshToken(string username, [FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository)
    {

        // var token = tokenRepository.FirstOrDefaultAsync( token => 
        //     token.User == username && token.Service == "Deezer"
        // );
        // if(token == null) return null;

        //! teoricamente, o token do deezer não expira
        throw new NotImplementedException();
    }
}