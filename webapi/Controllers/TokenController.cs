using Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.Core;
using Azure.ResourceManager.Storage;

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

    new BlobUtils().GenerateToken();

    return new TokenResponse
    {
      Token = "token"
    };
  }
}
