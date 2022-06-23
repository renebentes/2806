using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories;

public class Repository<TModel> where TModel : class
{
    private readonly SqlConnection _connection;

    public Repository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<TModel> GetAll()
    => _connection.GetAll<TModel>();
}
