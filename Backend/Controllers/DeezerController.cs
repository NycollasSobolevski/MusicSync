

// using music_api.DTO;
// using music_api.Model;

// namespace music_api.Controllers;


// [ApiController]
// [Route("[controller]")]
// [EnableCors("MainPolicy")]
// public class DeezerController : IStreamerController
// {
//     public string serverPort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     public string frontPort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     public string clientId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     public string clientSecret { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//     public string redirectCallback { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//     public Task<ActionResult> Callback([FromServices] HttpClient client, [FromBody] CallbackData data, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<ActionResult<StringReturn>> Get([FromBody] JWT data, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<ActionResult> getMoreTracks([FromBody] JWTWithData body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
//     {
//         throw new NotImplementedException();
//     }

//     public Task GetMusicData([FromServices] HttpClient client, [FromBody] string accessToken)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<ActionResult> GetPlaylist([FromQuery(Name = "id")] string id, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<ActionResult> GetPlaylistTracks([FromQuery(Name = "id")] string id, [FromQuery(Name = "streamer")] string streamer, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<ActionResult> GetUserPlaylists([FromBody] JWTWithGetPlaylistData data, [FromServices] IJwtService jwt, [FromServices] IRepository<User> userRepository, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<SpotifyUserData> getUserSpotify([FromServices] HttpClient client, string token)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<ActionResult> LogOff([FromBody] JWT data, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<ActionResult> RefreshToken([FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwtService, [FromBody] JWT jwt)
//     {
//         throw new NotImplementedException();
//     }

//     public Task refreshToken(string username, [FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository)
//     {
//         throw new NotImplementedException();
//     }

//     [HttpGet("Search")]
//     public void Search (
        
//     )
//     {
//         System.Console.WriteLine("entrou");
//     }

//     [HttpGet("Search/{id}")]
//     public void SearchById (
//         string id
//     )
//     {
//         System.Console.WriteLine("entrou");
//     }
// }