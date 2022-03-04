using System.ComponentModel.DataAnnotations;
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

public record UserCreateDTO
{
    [JsonPropertyName("first_name")]
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    [MaxLength(255)]
    public string Email { get; set; }

    [JsonPropertyName("gender")]
    [Required]
    [MaxLength(6)]
    public string Gender { get; set; } // male, female

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; } // 18 years of age
}

public record UserUpdateDTO
{
    [JsonPropertyName("last_name")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    public long? Mobile { get; set; } = null;

    [JsonPropertyName("email")]
    [MaxLength(255)]
    public string Email { get; set; }
}