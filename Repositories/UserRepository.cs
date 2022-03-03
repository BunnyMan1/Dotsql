using Dotsql.Models;
using Dapper;
using System.Data;

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

    public async Task<User> Create(User item)
    {        
        User result1 = new User();
        using IDbConnection connection = NewConnection;
        try
        {
            connection.Open();
            string query = $"INSERT INTO \"User\" VALUES ({item.EmployeeNumber}, '{item.FirstName}', '{item.LastName}', CURRENT_TIMESTAMP, '{item.Mobile}', '{item.Email}', '{item.Gender}');";

            var result = await connection.ExecuteAsync(query, commandType:CommandType.Text);
            
            string selectById = $"select * from \"User\" where \"EmployeeNumber\" = {item.EmployeeNumber}";
            var newuser = await connection. QueryAsync<User>(selectById, commandType:CommandType.Text);
            foreach(User user in newuser){
                result1 = user;
            }
        }
        catch(Exception ex)
        {            

        }
        finally{
            connection.Close();

        }
        return result1;
    }

    public Task Delete(long EmployeeNumber)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(long EmployeeNumber)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task Update(User Item)
    {
        throw new NotImplementedException();
    }
}