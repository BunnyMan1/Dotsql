using Dotsql.DTOs;

namespace Dotsql.Models;

public enum Gender
{
    Male = 1, // "Male" -> "male"
    Female = 2, // "Female" -> "female"
}

public record User
{
    /// <summary>
    /// Primary Key - NOT NULL, Unique, Index is Available
    /// </summary>
    public long EmployeeNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long Mobile { get; set; }

    /// <summary>
    /// Can be NULL
    /// </summary>
    public string Email { get; set; }
    public Gender Gender { get; set; }

    public UserDTO asDto => new UserDTO
    {
        EmployeeNumber = EmployeeNumber,
        FirstName = FirstName,
        LastName = LastName,
        Mobile = Mobile,
        Email = Email,
        Gender = Gender.ToString().ToLower(),
    };
}