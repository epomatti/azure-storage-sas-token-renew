# Azure Storage Blob SAS token

Implementation of the example [code](https://learn.microsoft.com/en-us/azure/storage/blobs/sas-service-create-dotnet?tabs=dotnet) for generating SAS tokens to access Blob storage containers.

Start by creating the storage account with a container:

```
az group create -n rgsas -l eastus
az storage account create -n stsasmgmt789 -g rgsas -l eastus --sku Standard_LRS --allow-blob-public-access true
az storage container create -n companyfiles --account-name stsasmgmt789
```

In the `webapi` directory, create an `.env` file and add the required values:

```sh
ACCESS_KEY_OPERATION_MODE="ENVIRONMENT_VARIABLE"
DEFAULT_ACCESS_KEY="key1"
ACCESS_KEY_1="xxxxxxx"
ACCESS_KEY_2=""
```

Start the application:

```sh
dotnet restore
dotnet run
```

Send a POST request to `http://localhost:5224/api/token` with the sample body:

```json
{
	"storageAccountName": "stsasmgmt789",
	"blobContainerName": "companyfiles",
	"expirationDate": "2030-12-21"
}
```
