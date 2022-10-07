using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories;

public class UserRepository : Repository<User>
{
    private readonly SqlConnection _connection;

    public UserRepository(SqlConnection connection)
        : base(connection)
    {
        _connection = connection;
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

    public override void Create(User user)
    {
        using var transaction = _connection.BeginTransaction();
        try
        {
            user.Id = 0;
            _connection.Insert(user, transaction);

            var insertUserRole = @"
                INSERT INTO
                    [UserRole]
                VALUES (
                    @UserId,
                    @RoleId
                )";

            foreach (var role in user.Roles)
            {
                _connection.Execute(insertUserRole, new
                {
                    userId = user.Id,
                    roleId = role.Id
                }, transaction);
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
