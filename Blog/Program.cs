using Blog.Models;
using Blog.Repositories;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

using var connection = new SqlConnection(connectionString);

ReadUsers(connection);
CreateUser(connection);
ReadRoles(connection);

static void ReadUsers(SqlConnection connection)
{
    var repository = new Repository<User>(connection);
    var users = repository.GetAll();

    foreach (var user in users)
    {
        Console.WriteLine(user.Name);

        foreach (var role in user.Roles)
        {
            Console.WriteLine($" - {role.Name}");
        }
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

static void CreateUser(SqlConnection connection)
{
    var user = new User
    {
        Name = "Equipe Balta.io",
        Bio = "Equipe Balata.io",
        Email = "hello@bata.io",
        PasswordHash = "HASH",
        Image = "https://...",
        Slug = "equipe-balta-io"
    };

    var repository = new UserRepository(connection);
    repository.Create(user);
    Console.WriteLine("Usuário criado com sucesso!");
}
