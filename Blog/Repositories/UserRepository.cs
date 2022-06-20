using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories;

public class UserRepository
{
    private readonly SqlConnection _connection;

    public UserRepository(SqlConnection connection) => _connection = connection;

    public IEnumerable<User> GetAll() => _connection.GetAll<User>();
}
