namespace webapi;

/// Defines where the system will get the access keys from
public enum AccessKeyOperationMode
{
  /// Reads the access keys from environment variables
  EnvironmentVariables,
  /// Get the access keys from the storage account
  StorageAccount
}