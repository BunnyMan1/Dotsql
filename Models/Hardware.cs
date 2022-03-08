using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotsql.Models;

public enum HardwareType
{
    Laptop = 1,
    CPU = 2,
    Monitor = 3,
    Accessory = 4,
    Smartphone = 5,
}

public record Hardware
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("mac_address")]
    public string MacAddress { get; set; } = null;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HardwareType Type { get; set; }

    [JsonPropertyName("user_employee_number")]
    public long UserEmployeeNumber { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
}

public record HardwareCreateDTO
{
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

    [JsonPropertyName("mac_address")]
    public string MacAddress { get; set; } = null;

    [JsonPropertyName("type")]
    [Required]
    [Range(1, 5)]
    public int Type { get; set; }

    [JsonPropertyName("user_employee_number")]
    [Required]
    public long UserEmployeeNumber { get; set; }
}