namespace webapi;

public class TokenService
{

  private ConfigService _configService;

  public TokenService(ConfigService configService)
  {
    this._configService = configService;
  }

  public string GenerateToken()
  {
    return "";
  }

}