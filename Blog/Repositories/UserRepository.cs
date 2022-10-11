using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Blog.Repositories;

public class UserRepository : Repository<User>
{
    private readonly SqlConnection _connection;

    public UserRepository(SqlConnection connection)
        : base(connection)
    {
        _connection = connection;
    }

    public void AddRole(int userId, int roleId, IDbTransaction? transaction = null)
    {
        var insertUserRole = @"
                INSERT INTO
                    [UserRole]
                VALUES (
                    @UserId,
                    @RoleId
                )";

        _connection.Execute(insertUserRole, new
        {
            userId,
            roleId
        }, transaction);
    }

    public IEnumerable<User> GetAllWithRoles()
    {
        const string query = @"
            SELECT
                [User].*,
                [Role].*
            FROM
                [User]
                LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";

        var users = new List<User>();
        var items = _connection.Query<User, Role, User>(
            query,
            (user, role) =>
            {
                var usr = users.Find(x => x.Id == user.Id);

                if (usr is null)
                {
                    usr = user;

                    if (role is not null)
                        usr.Roles.Add(role);

                    users.Add(usr);
                }
                else
                {
                    usr.Roles.Add(role);
                }

                return user;
            }, splitOn: "Id");

        return users;
    }

    public User? GetByIdWithRoles(int id)
    {
        const string query = @"
            SELECT
                [User].*,
                [Role].*
            FROM
                [User]
                LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]
            WHERE
                [User].[Id] = @id";

        var users = new List<User>();
        var items = _connection.Query<User, Role, User>(
            query,
            (user, role) =>
            {
                var usr = users.Find(x => x.Id == user.Id);

                if (usr is null)
                {
                    usr = user;

                    if (role is not null)
                        usr.Roles.Add(role);

                    users.Add(usr);
                }
                else
                {
                    usr.Roles.Add(role);
                }

                return user;
            }, new { id }, splitOn: "Id");

        return users.SingleOrDefault();
    }

    public bool HasRole(int userId, int roleId, IDbTransaction? transaction = null)
    {
        const string query = @"
            SELECT
                COUNT(1)
            FROM
                [UserRole]
            WHERE
                [UserId] = @userId AND
                [RoleId] = @roleId";

        return _connection.ExecuteScalar<bool>(query, new { userId, roleId }, transaction);
    }

    public override void Create(User user)
    {
        using var transaction = _connection.BeginTransaction();
        try
        {
            user.Id = 0;
            _connection.Insert(user, transaction);

            foreach (var role in user.Roles)
            {
                AddRole(user.Id, role.Id, transaction);
            }
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public override void Update(User user)
    {
        using var transaction = _connection.BeginTransaction();
        try
        {
            if (user.Id != 0)
            {
                _connection.Update(user, transaction);

                foreach (var role in user.Roles)
                {
                    if (!HasRole(user.Id, role.Id, transaction))
                    {
                        AddRole(user.Id, role.Id, transaction);
                    }
                }
            }
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}
