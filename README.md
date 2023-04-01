# azure-storage-sas-token-renew

https://learn.microsoft.com/en-us/azure/storage/blobs/sas-service-create-dotnet?tabs=dotnet


```
az group create -n rgsas -l eastus
az storage account create -n stsasmgmt789 -g rgsas -l eastus --sku Standard_LRS --allow-blob-public-access
az storage container create -n companyfiles --account-name stsasmgmt789
```


