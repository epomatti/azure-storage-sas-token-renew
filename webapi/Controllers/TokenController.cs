using Microsoft.AspNetCore.Mvc;

namespace webapi;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{

  private readonly ILogger<TokenController> _logger;

  public TokenController(ILogger<TokenController> logger)
  {
    _logger = logger;
  }

  [HttpPost(Name = "RenewToken")]
  public TokenResponse Post()
  {
    return new TokenResponse
    {
      Token = "token"
    };
  }
}
