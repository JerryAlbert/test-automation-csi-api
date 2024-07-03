using System.ComponentModel.DataAnnotations;

namespace test_automation_csi_api.Model;

public class DataConsumerCredentials
{
    /// <summary>
    /// Gets or sets the user identifier (DataConsumerName_[UniqueId])
    /// </summary>
    [Required]
    public string? UserToken { get; set; }
        
    /// <summary>
    /// Gets or sets the API Key
    /// </summary>
    [Required]
    public string? ApiKey { get; set; }
}