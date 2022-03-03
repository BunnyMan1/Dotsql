using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _repository;

    public UserController(ILogger<UserController> logger, IUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost]
    public async Task<User> Index([FromBody]User user)
    {
        User response = await _repository.Create(user);
        return response;
    }

    // [HttpGet]
    // public async Task<List<User>> Index()
    // {
    //     // Gets list of users.
    // }

    // [HttpDelete]
    // public async Task<List<User>> Index([FromQuery] long EmployeeNumber)
    // {
    //     // Delete 
    // }

    // [HttpPut]
    // public async Task<int> Index(User user)
    // {
    //     //Updates record
    // }

    // [HttpGet]
    // [Route("{id:long}")]
    // public async Task<List<User>> Index([FromRoute] long id)
    // {
    //     //
    // }

}
