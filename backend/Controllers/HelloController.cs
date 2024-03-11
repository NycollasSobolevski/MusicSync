[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class HelloController 
{
    [HttpGet]
    public string Get()
    {
        return "Hello World!";
    }

    [HttpPost]
    public string Post(
        [FromBody] string body
    )
    {
        return body;
    }
}