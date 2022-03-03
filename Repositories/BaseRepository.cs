using System.Data;
using Dotsql.Settings;
using Npgsql;

namespace Dotsql.Repositories;

public class BaseRepository
{
    private readonly IConfiguration _configuration;
    public BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // public NpgsqlConnection NewConnection => new NpgsqlConnection(_configuration
    //     .GetSection(nameof(PostgresSettings)).Get<PostgresSettings>().ConnectionString);

        public NpgsqlConnection NewConnection => new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=12345;Database=TestDb;Include Error Detail=true");
}
