using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/hardware")]
public class HardwareController : ControllerBase
{
    private readonly ILogger<HardwareController> _logger;
    private readonly IHardwareRepository _hardware;
    private readonly IUserRepository _user;

    public HardwareController(ILogger<HardwareController> logger,
    IHardwareRepository hardware, IUserRepository user)
    {
        _logger = logger;
        _hardware = hardware;
        _user = user;
    }

    [HttpPost]
    public async Task<ActionResult<Hardware>> CreateHardware([FromBody] HardwareCreateDTO Data)
    {
        var user = await _user.GetById(Data.UserEmployeeNumber);
        if (user is null)
            return NotFound("No user found with given employee number");

        var toCreateHardware = new Hardware
        {
            Name = Data.Name.Trim(),
            MacAddress = Data.MacAddress?.Trim(),
            Type = (HardwareType)Data.Type,
            UserEmployeeNumber = Data.UserEmployeeNumber,
        };

        var createdItem = await _hardware.Create(toCreateHardware);

        return StatusCode(StatusCodes.Status201Created, createdItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateHardware([FromRoute] int id,
    [FromBody] HardwareCreateDTO Data)
    {
        var existing = await _hardware.GetById(id);
        if (existing is null)
            return NotFound("No hardware found with given id");

        var toUpdateItem = existing with
        {
            Name = Data.Name.Trim(),
            MacAddress = Data.MacAddress?.Trim(),
            Type = (HardwareType)Data.Type,
            UserEmployeeNumber = Data.UserEmployeeNumber,
        };

        await _hardware.Update(toUpdateItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHardware([FromRoute] int id)
    {
        var existing = await _hardware.GetById(id);
        if (existing is null)
            return NotFound("No hardware found with given id");

        await _hardware.Delete(id);

        return NoContent();
    }
}
