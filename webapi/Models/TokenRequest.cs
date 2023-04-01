using System.ComponentModel.DataAnnotations;

namespace webapi;

public class TokenRequest
{
  [Required]
  public string StorageAccountName { get; set; } = "";
  [Required]
  public string BlobContainerName { get; set; } = "";
  [Required]
  public DateTime? ExpirationDate { get; set; }
}
