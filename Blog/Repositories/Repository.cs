using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories;

public class Repository<TModel> where TModel : ModelBase
{
    private readonly SqlConnection _connection;

    public Repository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<TModel> GetAll()
        => _connection.GetAll<TModel>();

    public TModel GetById(int id)
        => _connection.Get<TModel>(id);

    public void Create(TModel model)
    {
        model.Id = 0;
        _connection.Insert(model);
    }

    public void Update(TModel model)
    {
        if (model.Id != 0)
        {
            _connection.Update(model);
        }
    }

    public void Delete(TModel model)
    {
        if (model.Id != 0)
        {
            _connection.Delete(model);
        }
    }

    public void Delete(int id)
    {
        if (id == 0)
        {
            return;
        }

        var model = GetById(id);
        Delete(model);
    }
}
