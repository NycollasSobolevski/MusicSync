

namespace music_api.Controllers;


[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class DeezerController : ControllerBase
{
    [HttpGet("Search")]
    public void Search (
        
    )
    {
        System.Console.WriteLine("entrou");
    }

    [HttpGet("Search/{id}")]
    public void SearchById (
        string id
    )
    {
        System.Console.WriteLine("entrou");
    }
}