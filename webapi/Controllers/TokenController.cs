using Microsoft.AspNetCore.Mvc;

namespace webapi;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{

  private readonly ILogger<TokenController> _logger;

  public TokenController(ILogger<TokenController> logger)
  {
    _logger = logger;
  }

  [HttpGet(Name = "GetWeatherForecast")]
  public TokenResponse Get()
  {
    return new TokenResponse
    {
      Token = "token"
    };
  }
}
