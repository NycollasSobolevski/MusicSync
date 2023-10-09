
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using music_api;
using music_api.DTO;
using music_api.Model;


[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class DeezerController : StreamerController
{
    [HttpPost("Callback")]
    public override async Task<ActionResult> Callback([FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    ){
        throw new NotImplementedException();
    }

    [HttpPost("GetData")]
    public override async Task<ActionResult<StringReturn>> Get()
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetMoreTracks")]
    public override async Task<ActionResult> GetMoreTracks( )
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetMusicData")]
    public override async Task GetMusicData()
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetPlaylist")]
    public override async Task<ActionResult> GetPlaylist()
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetPlaylistTracks")]
    public override async Task<ActionResult> GetPlaylistTracks()
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetUserPlaylists")]
    public override async Task<ActionResult> GetUserPlaylists()
    {
        throw new NotImplementedException();
    }

    [HttpPost("LogOff")]
    public override async Task<ActionResult> LogOff()
    {
        throw new NotImplementedException();
    }

    [HttpPost("RefreshToken")]
    public override async Task<ActionResult> RefreshToken()
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetUserSpotify")]
    protected override Task<SpotifyUserData> GetUserSpotify()
    {
        throw new NotImplementedException();
    }

    [HttpPost("refreshToken")]
    protected override Task refreshToken()
    {
        throw new NotImplementedException();
    }
}