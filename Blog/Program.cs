using Blog.Models;
using Blog.Repositories;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

using var connection = new SqlConnection(connectionString);

ReadUsers(connection);
ReadRoles(connection);

static void ReadUsers(SqlConnection connection)
{
    var repository = new Repository<User>(connection);
    var users = repository.GetAll();

    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
}

static void ReadRoles(SqlConnection connection)
{
    var repository = new Repository<Role>(connection);
    var roles = repository.GetAll();

    foreach (var role in roles)
    {
        Console.WriteLine(role.Name);
    }
}
