using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Storage;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace webapi;

public class TokenService
{

  private ConfigService _configService;

  public TokenService(ConfigService configService)
  {
    this._configService = configService;
  }

  public string GenerateToken(string accountName, string containerName, DateTime expirationDate)
  {
    Uri blobContainerUri = new(string.Format("https://{0}.blob.core.windows.net/{1}", accountName, containerName));

    StorageSharedKeyCredential storageSharedKeyCredential = new(accountName, this._configService.AccessKey1);
    BlobContainerClient blobContainerClient = new(blobContainerUri, storageSharedKeyCredential);

    var sas = GetServiceSasUriForContainer(blobContainerClient, expirationDate);

    return sas.ToString();
  }

  private Uri GetServiceSasUriForContainer(BlobContainerClient containerClient, DateTime expirationDate)
  {
    // Check whether this BlobContainerClient object has been authorized with Shared Key.
    if (containerClient.CanGenerateSasUri)
    {
      BlobSasBuilder sasBuilder = new BlobSasBuilder()
      {
        BlobContainerName = containerClient.Name,
        Resource = "c"
      };

      sasBuilder.ExpiresOn = expirationDate;
      sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
      sasBuilder.SetPermissions(BlobContainerSasPermissions.List);

      return containerClient.GenerateSasUri(sasBuilder);
    }
    else
    {
      throw new Exception("Blob container client not authorized.");
    }
  }

  // TODO: Implement if required to get the key directly from the storage
  protected Array GetKeysFromStorage()
  {
    ArmClient armClient = new ArmClient(new DefaultAzureCredential());
    var id = "/subscriptions/{SUBSCRIPTION_ID}/resourceGroups/{RESOURCE_GROUP_ID}/providers/Microsoft.Storage/storageAccounts/{STORAGE_ACCOUNT_NAME}";
    var resourceIdentifier = new ResourceIdentifier(id);
    StorageAccountResource storage = armClient.GetStorageAccountResource(resourceIdentifier);
    return storage.GetKeys().ToArray();
  }
}
