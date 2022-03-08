using Dapper;
using Dotsql.Models;
using Dotsql.Utilities;

namespace Dotsql.Repositories;

public interface IHardwareRepository
{
    Task<Hardware> Create(Hardware Item);
    Task Update(Hardware Item);
    Task Delete(int Id);
    Task<List<Hardware>> GetAllForEmployee(long EmployeeNumber);
    Task<Hardware> GetById(int Id);
}

public class HardwareRepository : BaseRepository, IHardwareRepository
{
    public HardwareRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Hardware> Create(Hardware Item)
    {
        var query = $@"INSERT INTO {TableNames.hardware} (name, mac_address, type,
        user_employee_number) VALUES (@Name, @MacAddress, @Type, @UserEmployeeNumber) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Hardware>(query, Item);
    }

    public async Task Delete(int Id)
    {
        var query = $@"DELETE FROM {TableNames.hardware} WHERE id = @Id";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { Id });
    }

    public async Task<List<Hardware>> GetAllForEmployee(long EmployeeNumber)
    {
        var query = $@"SELECT * FROM {TableNames.hardware} 
        WHERE user_employee_number = @EmployeeNumber";

        using (var con = NewConnection)
            return (await con.QueryAsync<Hardware>(query, new { EmployeeNumber })).AsList();
    }

    public async Task<Hardware> GetById(int Id)
    {
        var query = $@"SELECT * FROM {TableNames.hardware} 
        WHERE id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Hardware>(query, new { Id });
    }

    public async Task Update(Hardware Item)
    {
        var query = $@"UPDATE {TableNames.hardware} SET name = @Name, mac_address = @MacAddress, 
        type = @Type, user_employee_number = @UserEmployeeNumber WHERE id = @Id";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, Item);
    }
}