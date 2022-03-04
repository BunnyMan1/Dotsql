using Dotsql.Models;
using Dapper;
using Dotsql.Utilities;

namespace Dotsql.Repositories;

public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User Item);
    Task<bool> Delete(long EmployeeNumber);
    Task<User> GetById(long EmployeeNumber);
    Task<List<User>> GetList();

}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<User> Create(User Item)
    {
        var query = $@"INSERT INTO ""{TableNames.user}"" 
        (first_name, last_name, date_of_birth, mobile, email, gender) 
        VALUES (@FirstName, @LastName, @DateOfBirth, @Mobile, @Email, @Gender) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<User>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long EmployeeNumber)
    {
        var query = $@"DELETE FROM ""{TableNames.user}"" 
        WHERE employee_number = @EmployeeNumber";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { EmployeeNumber });
            return res > 0;
        }
    }

    public async Task<User> GetById(long EmployeeNumber)
    {
        var query = $@"SELECT * FROM ""{TableNames.user}"" 
        WHERE employee_number = @EmployeeNumber";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<User>(query, new { EmployeeNumber });
    }

    public async Task<List<User>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.user}""";

        List<User> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<User>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    public async Task<bool> Update(User Item)
    {
        var query = $@"UPDATE ""{TableNames.user}"" SET first_name = @FirstName, 
        last_name = @LastName, date_of_birth = @DateOfBirth, mobile = @Mobile, 
        email = @Email, gender = @Gender WHERE employee_number = @EmployeeNumber";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}