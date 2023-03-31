using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Storage;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;

namespace webapi;

public class BlobUtils
{

  public void GenerateToken()
  {

    var storage = GetStorage();

    var keys = storage.GetKeys().ToArray();
    var key1 = keys[0];
    var key2 = keys[1];

    Console.WriteLine(key1.KeyName);
    Console.WriteLine(key1.Value);

    string AccountName = "oxizcuyvbawqfasdf";
    string AccountKey = keys[0].Value;
    string ContainerName = "test";

    Uri blobContainerUri = new(string.Format("https://{0}.blob.core.windows.net/{1}",
        AccountName, ContainerName));

    StorageSharedKeyCredential storageSharedKeyCredential =
        new(AccountName, AccountKey);

    BlobContainerClient blobContainerClient =
        new(blobContainerUri, storageSharedKeyCredential);

    var sas = GetServiceSasUriForContainer(blobContainerClient);

    Console.WriteLine(sas);
  }

  private static Uri GetServiceSasUriForContainer(BlobContainerClient containerClient,
                                            string storedPolicyName = null)
  {
    // Check whether this BlobContainerClient object has been authorized with Shared Key.
    if (containerClient.CanGenerateSasUri)
    {
      // Create a SAS token that's valid for one hour.
      BlobSasBuilder sasBuilder = new BlobSasBuilder()
      {
        BlobContainerName = containerClient.Name,
        Resource = "c"
      };

      if (storedPolicyName == null)
      {
        sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
        sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
        sasBuilder.SetPermissions(BlobContainerSasPermissions.List);
      }
      else
      {
        sasBuilder.Identifier = storedPolicyName;
      }

      Uri sasUri = containerClient.GenerateSasUri(sasBuilder);
      Console.WriteLine("SAS URI for blob container is: {0}", sasUri);
      Console.WriteLine();

      return sasUri;
    }
    else
    {
      Console.WriteLine(@"BlobContainerClient must be authorized with Shared Key 
                          credentials to create a service SAS.");
      return null;
    }
  }

  protected StorageAccountResource GetStorage()
  {
    ArmClient armClient = new ArmClient(new DefaultAzureCredential());
    var id = "/subscriptions/2ea97ae3-d129-41fb-a4ca-eb56ad392d35/resourceGroups/rg-test/providers/Microsoft.Storage/storageAccounts/oxizcuyvbawqfasdf";
    var resourceIdentifier = new ResourceIdentifier(id);
    StorageAccountResource storage = armClient.GetStorageAccountResource(resourceIdentifier);
    return storage;
  }

}