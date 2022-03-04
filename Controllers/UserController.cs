using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _user;

    public UserController(ILogger<UserController> logger, IUserRepository user)
    {
        _logger = logger;
        _user = user;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
    {
        var usersList = await _user.GetList();

        // User -> UserDTO
        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{employee_number}")]
    public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] long employee_number)
    {
        var user = await _user.GetById(employee_number);

        if (user is null)
            return NotFound("No user found with given employee number");

        return Ok(user.asDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserCreateDTO Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            return BadRequest("Gender value is not recognized");

        var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        if (subtractDate.TotalDays / 365 < 18.0)
            return BadRequest("Employee must be at least 18 years old");

        var toCreateUser = new User
        {
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Email = Data.Email.Trim().ToLower(),
            Gender = Enum.Parse<Gender>(Data.Gender, true),
            Mobile = Data.Mobile,
        };

        var createdUser = await _user.Create(toCreateUser);

        return StatusCode(StatusCodes.Status201Created, createdUser.asDto);
    }

    [HttpPut("{employee_number}")]
    public async Task<ActionResult> UpdateUser([FromRoute] long employee_number,
    [FromBody] UserUpdateDTO Data)
    {
        var existing = await _user.GetById(employee_number);
        if (existing is null)
            return NotFound("No user found with given employee number");

        var toUpdateUser = existing with
        {
            Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            LastName = Data.LastName?.Trim() ?? existing.LastName,
            Mobile = Data.Mobile ?? existing.Mobile,
            DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _user.Update(toUpdateUser);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }

    [HttpDelete("{employee_number}")]
    public async Task<ActionResult> DeleteUser([FromRoute] long employee_number)
    {
        var existing = await _user.GetById(employee_number);
        if (existing is null)
            return NotFound("No user found with given employee number");

        var didDelete = await _user.Delete(employee_number);

        return NoContent();
    }
}
