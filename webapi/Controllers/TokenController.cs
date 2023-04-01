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
    DateTime expirationDate = (DateTime) request.ExpirationDate!;
    bool valid = expirationDate - DateTime.Now > TimeSpan.FromHours(2);
    if(!valid) {
      return BadRequest("Date must be a future date.");
    }
    var token = _tokenService.GenerateToken(request.StorageAccountName, request.BlobContainerName, (DateTime)request.ExpirationDate!);
    var response = new TokenResponse
    {
      Token = token
    };
    return Ok(response);
  }
}
