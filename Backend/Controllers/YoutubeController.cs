using music_api.DTO;

namespace music_api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class YoutubeController : ControllerBase 
{
    [HttpGet("GetAccessToken")]
    public string Get()
    {
        

        return "";
    }
}