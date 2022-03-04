using Dotsql.Models;
using Dapper;

namespace Dotsql.Repositories;

public interface IUserRepository
{
    Task<User> Create(User Item);
    Task Update(User Item);
    Task Delete(long EmployeeNumber);
    Task<User> GetById(long EmployeeNumber);
    Task<List<User>> GetList();

}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {

    }

    public Task<User> Create(User Item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long EmployeeNumber)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(long EmployeeNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""user""";

        List<User> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<User>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    public Task Update(User Item)
    {
        throw new NotImplementedException();
    }
}