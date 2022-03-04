using System.Text.Json.Serialization;

namespace Dotsql.DTOs;

public record UserDTO
{
    [JsonPropertyName("employee_number")]
    public long EmployeeNumber { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }
}