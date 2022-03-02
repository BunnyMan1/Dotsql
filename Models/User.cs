namespace Dotsql.Models;

public enum Gender
{
    Male = 1,
    Female = 2,
}

public record User
{    
    public long EmployeeNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
}