namespace webapi;

public class ConfigService
{

  public AccessKeyOperationMode accessKeyOperationMode { get; set; } = AccessKeyOperationMode.StorageAccount;
  public string DefaultAccessKey { get; set; } = "key1";
  public string AccessKey1 { get; set; } = "";
  public string AccessKey2 { get; set; } = "";

  public ConfigService()
  {
    _AccessKeyOperationMode();
  }

  protected void _AccessKeyOperationMode()
  {
    var env = Environment.GetEnvironmentVariable("ACCESS_KEY_OPERATION_MODE");
    switch (env)
    {
      case "ENVIRONMENT_VARIABLE":
        // Use default
        break;
      case "STORAGE_ACCOUNT":
        this.accessKeyOperationMode = AccessKeyOperationMode.StorageAccount;
        break;
    }
  }

  protected void _DefaultAccessKey()
  {
    string env = Environment.GetEnvironmentVariable("DEFAULT_ACCESS_KEY")!;
    var set = new HashSet<string>() { "key1", "key2" };

    if (set.Contains(env))
    {
      this.DefaultAccessKey = env;
    }
    else
    {
      throw new Exception($"Invalid access key identifier: {env}");
    }
  }

  protected void _AccessKeys()
  {
    this.AccessKey1 = Environment.GetEnvironmentVariable("ACCESS_KEY_1")!;
    this.AccessKey2 = Environment.GetEnvironmentVariable("ACCESS_KEY_2")!;
  }

}