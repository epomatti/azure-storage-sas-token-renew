using Microsoft.AspNetCore.Mvc;

namespace webapi;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
  private TokenService _tokenService;

  public TokenController(TokenService tokenService)
  {
    this._tokenService = tokenService;
  }

  [HttpPost]
  public IActionResult Post(TokenRequest request)
  {
    var token = _tokenService.GenerateToken();
    var response = new TokenResponse
    {
      Token = token
    };
    return Ok(response);
  }
}
