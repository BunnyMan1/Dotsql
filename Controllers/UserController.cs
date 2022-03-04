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
    public async Task GetUserById()
    {

    }

    [HttpPost]
    public async Task CreateUser()
    {

    }

    [HttpPut]
    public async Task UpdateUser()
    {

    }

    [HttpDelete]
    public async Task DeleteUser()
    {

    }
}
