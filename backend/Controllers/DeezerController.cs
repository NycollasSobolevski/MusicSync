
using System.Net;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;
using music_api;
using music_api.Controllers;
using music_api.DTO;
using music_api.Model;
using SpotifyAPI.Web;
using ZstdSharp.Unsafe;
using Microsoft.Extensions.Configuration.UserSecrets;

#pragma warning disable
[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class DeezerController : StreamerController
{   
    public DeezerController()
    {
        this.clientUrl = Environment.GetEnvironmentVariable("DEEZER_CLIENT_URL");
        this.clientId = Environment.GetEnvironmentVariable("DEEZER_CLIENT_ID");
        this.clientSecret = Environment.GetEnvironmentVariable("DEEZER_CLIENT_SECRET");
        this.redirectCallback = $"{Environment.GetEnvironmentVariable("FRONTEND_URI")}/Callback?streamer=deezer";
        this.streamer = "Deezer";
        this.fronturi = Environment.GetEnvironmentVariable("FRONTEND_URI");
    }
    private readonly string clientUrl;
    private readonly string clientId;
    private readonly string clientSecret;
    private readonly string redirectCallback;
    private readonly string streamer;
    private readonly string fronturi;


    /// <summary>
    ///     Decide se a primeira interação com o usuario seria conectar com o cliente ou se ja estiver conectado, redireciona-lo a outra aba do frontend
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userRepository"></param>
    /// <param name="tokenRepository"></param>
    /// <param name="jwt"></param>
    /// <returns></returns>
    [HttpPost("GetData")]
    public override async Task<ActionResult<StringReturn>> Get(
        bool debug, [FromBody] JWT data,[FromServices] IRepository<User> userRepository,[FromServices] IRepository<Token> tokenRepository,[FromServices] IJwtService jwt
    )
    {

        try{
            var userJwt = jwt.Validate<UserJwtData>(data.Value);
            var user = await userRepository.FirstOrDefaultAsync( user => 
                user.Name == userJwt.Name
            );

            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == user.Name && token.Service == this.streamer
            );
            System.Console.WriteLine(token);
            if(token != null){
                return Ok(new StringReturn{
                    Data = $"{this.fronturi}/?tab=Deezer"
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

    /// <summary>
    ///     Executada quando se retorna do login com o cliente aonde salva o token do usuario no banco
    /// </summary>
    /// <param name="client"></param>
    /// <param name="data"></param>
    /// <param name="tokenRepository"></param>
    /// <param name="userRepository"></param>
    /// <param name="jwt"></param>
    /// <returns></returns>
    [HttpPost("Callback")]
    public override async Task<ActionResult> Callback(
        bool debug, [FromServices] HttpClient client,[FromBody] CallbackData data,[FromServices] IRepository<Token> tokenRepository,[FromServices] IRepository<User> userRepository,[FromServices] IJwtService jwt
    ){
        try
        {
            if(debug)
                System.Console.WriteLine(data.code);

            var userJwt = jwt.Validate<UserJwtData>(data.jwt);
            var user = await userRepository.FirstOrDefaultAsync( user => 
                user.Name == userJwt.Name
            );

            string url = $"https://connect.deezer.com/oauth/access_token.php?app_id={this.clientId}&secret={this.clientSecret}&code={data.code}&output=json";
            
            if(debug)
                System.Console.WriteLine(url);
            
            var response = await client.GetAsync(url);

            if(response.StatusCode != HttpStatusCode.OK){
                return BadRequest("Error getting token");
            }
            var tokenData = await response.Content.ReadFromJsonAsync<DeezerToken>();
            
            if(debug)
                System.Console.WriteLine(tokenData);
            Token _token = new() 
            {
                User = user.Name,
                Service = this.streamer,
                ServiceToken = tokenData.access_token,
                ExpiresIn = tokenData.expires,
                LastUpdate = DateTime.Now
            };
            if(debug)
                System.Console.WriteLine($"Token: {_token.ServiceToken}\nRefreshToken: {_token.RefreshToken}\nExpiresIn: {_token.ExpiresIn}\nLastUpdate: {_token.LastUpdate}");
            await tokenRepository.add(_token);

            return Ok();

        } catch (Exception e){
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }

    
    [HttpPost("GetMoreTracks")]
    public override async Task<ActionResult> GetMoreTracks(
        bool debug, [FromBody] JWTWithData<string> body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetMusicData")]
    public override async Task GetMusicData(
        bool debug, [FromServices] HttpClient client, [FromBody] string accessToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetPlaylist/")]
    public override async Task<ActionResult> GetPlaylist(
        bool debug, [FromQuery(Name = "id")] string id, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        try
        {
            var userId = jwt.Validate<UserJwtData>(body.Value);
            var token = await tokenRepository.FirstOrDefaultAsync(token => 
                token.User == userId.Name &&
                token.Service == this.streamer
            );
            if(token == null)
                return NotFound("token not found");

            var req = await client.GetAsync($"https://api.deezer.com/playlist/{id}");
            var reqData = await req.Content.ReadFromJsonAsync<DeezerPlaylist>();
            
            var result = new PlaylistData(){
                Name = reqData.title,
                Description = "",
                Id = reqData.id.ToString(),
                Href = reqData.link,
                Images = new List<Images>(){
                    new Images{
                        width=500,
                        height=500,
                        url=reqData.picture_big
                    },                    
                    new Images{
                        width=56,
                        height=56,
                        url=reqData.picture_small
                    },
                    new Images{
                        width=120,
                        height=120,
                        url=reqData.picture
                    },
                    new Images{
                        width=250,
                        height=250,
                        url=reqData.picture_medium
                    },
                },
                Owner = new PlaylistOwner(){
                    display_name = reqData.creator.name,
                    id = reqData.creator.id.ToString(),
                },
                TracksTotal = new HrefTracks(){
                    href = reqData.tracklist,
                    total = reqData.nb_tracks
                }
            };
            return Ok(result);
        }
        catch (Exception exp)
        {
            System.Console.WriteLine(exp.Message);
            return BadRequest("Unknow server error");
        }
    }

    [HttpPost("GetPlaylistTracks/")]
    public override async Task<ActionResult> GetPlaylistTracks(
        bool debug, [FromQuery(Name = "id")] string id, [FromQuery(Name = "streamer")] string streamer, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client,[FromServices] IRepository<User> userRepository
    ){
        var user = await UserTools.ValidateUser(userRepository, jwt, body.Value);
        if(user == null) return BadRequest("Invalid user");
        try
        {
            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == user.Name && token.Service == "Deezer"
            );
            if(token == null) return NotFound("Token not found");

            string url = $"https://api.deezer.com/playlist/{id}?access_token={token.ServiceToken}";
            var res = await client.GetAsync(url);
            var playlist = await res.Content.ReadFromJsonAsync<DeezerPlaylistsData>();
            
            if(debug){
                System.Console.WriteLine(playlist.data);
                string stt= await res.Content.ReadAsStringAsync();
                System.Console.WriteLine(stt);
            }
            
            return Ok(await res.Content.ReadAsStringAsync());
        }
        catch (System.Exception e)
        {   
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }

    [HttpPost("GetUserPlaylists")]
    public override async Task<ActionResult> GetUserPlaylists(
        bool debug, [FromBody] JWTWithGetPlaylistData data, [FromServices] IJwtService jwt, [FromServices] IRepository<User> userRepository, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client
    ){
        

        try{
            var _user = await UserTools.ValidateUser(userRepository, jwt, data.Jwt.Value);
            
            if(User == null) return BadRequest("Invalid user");
            
            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == _user.Name && token.Service == this.streamer
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
    public override async Task<ActionResult> LogOff(
        bool debug, [FromBody] JWT data, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository)
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

    [HttpPost("createPlaylist")]
    public override async Task<ActionResult> CreatePlaylist(
        bool debug, [FromBody] UserCreatePlaylistWithJwt data, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] HttpClient client
    )
    {
        System.Console.WriteLine("create playlist");
        try{
            var user = await UserTools.ValidateUser(userRepository, jwt, data.Jwt.Value);
            if(user == null) return BadRequest("Invalid user");
            var token = await UserTools.GetUserToken(tokenRepository, user.Name, this.streamer);
            if(token == null) return Unauthorized("Token not found");

            var deezerUser = await this.GetUserData(client, token.ServiceToken);
            if(deezerUser == null) return BadRequest("Error getting user data");
            string uri = $"https://api.deezer.com/user/{deezerUser.id}/playlists?title={data.Data.name}&access_token={token.ServiceToken}";
            var res = await client.PostAsync(uri, null);
            if(res.StatusCode != HttpStatusCode.OK) return BadRequest("Error creating playlist");

            PlaylistCreateReturn playlistId = await res.Content.ReadFromJsonAsync<PlaylistCreateReturn>();

            return Ok(playlistId);

        }
        catch (Exception e) {
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }

    [HttpPost("AddTrackToPlaylist")]
    public override async Task<ActionResult> AddTrackToPlaylist(
        bool debug, [FromBody] JWTWithData<TrackAndPlaylist> data, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] HttpClient client
    ){
        try{

            var user = await UserTools.ValidateUser(userRepository, jwt, data.Jwt.Value);
            if(user == null) return Unauthorized("Invalid user");
            var token = await UserTools.GetUserToken(tokenRepository, user.Name, this.streamer);
            if(token == null) return Unauthorized("Token not found");

            var deezerUser = await this.GetUserData(client, token.ServiceToken);
            if(deezerUser == null) return BadRequest("Error getting user data");
            

            //! search track
            data.Data.Track.name = WebUtility.UrlEncode(data.Data.Track.name);
            data.Data.Track.author = WebUtility.UrlEncode(data.Data.Track.author);
            string uri = $"https://api.deezer.com/search/track?q={data.Data.Track.author}%2520{data.Data.Track.name}&access_token={token.ServiceToken}";
            var res = await client.GetAsync(uri);

            if(res.StatusCode != HttpStatusCode.OK) return BadRequest("Error searching track");

            var searchResult = await res.Content.ReadFromJsonAsync<SearchData>();
            SearchTrackData track = searchResult.data[0];
            //! add to playlist
            uri = $"https://api.deezer.com/playlist/{data.Data.PlaylistId}/tracks?songs={track.id}&access_token={token.ServiceToken}";
            res = await client.PostAsync(uri, null);
            string stt = await res.Content.ReadAsStringAsync();
            

            return Ok(stt);

        } catch (Exception e){
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }

    [HttpPost("RefreshToken")]
    public override async Task<ActionResult> RefreshToken(
        bool debug, [FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwtService, [FromBody] JWT jwt)
    {
        throw new NotImplementedException();
    }

    protected override async Task<DeezerUserData> GetUserData([FromServices] HttpClient client, string token)
    {
        try{
            string url = $"https://api.deezer.com/user/me?access_token={token}";
            var res = await client.GetAsync(url);

            if(res.StatusCode != HttpStatusCode.OK) return null;

            var userdata = await res.Content.ReadFromJsonAsync<DeezerUserData>();
            return userdata;

        } catch (Exception e) {
            System.Console.WriteLine(e);
            return null;
        }

    }

    protected override Task refreshToken(string username, [FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository)
    {

        // var token = tokenRepository.FirstOrDefaultAsync( token => 
        //     token.User == username && token.Service == this.streamer
        // );
        // if(token == null) return null;

        //! teoricamente, o token do deezer não expira
        throw new NotImplementedException();
    }

}