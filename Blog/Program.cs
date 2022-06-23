using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

using var connection = new SqlConnection(connectionString);

ReadUsers(connection);
ReadRoles(connection);

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

static void ReadUsers(SqlConnection connection)
{
    var repository = new Repository<User>(connection);
    var users = repository.GetAll();

    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
}

static void ReadUser(SqlConnection connection)
{
    var repository = new UserRepository(connection);
    var user = repository.GetById(1);
    Console.WriteLine(user.Name);
}

static void UpdateUser(SqlConnection connection)
{
    var user = new User
    {
        Id = 1,
        Name = "Equipe | Balta.io",
        Bio = "Equipe Balata.io",
        Email = "hello@bata.io",
        PasswordHash = "HASH",
        Image = "https://...",
        Slug = "equipe-balta-io"
    };

    var repository = new UserRepository(connection);
    repository.Update(user);
    Console.WriteLine("Usuário atualizado com sucesso!");
}

static void DeleteUser(SqlConnection connection)
{
    var repository = new UserRepository(connection);
    var user = repository.GetById(1);
    repository.Delete(user);
    Console.WriteLine("Usuário apagado com sucesso!");
}

static void ReadRoles(SqlConnection connection)
{
    var repository = new RoleRepository(connection);
    var roles = repository.GetAll();

    foreach (var role in roles)
    {
        Console.WriteLine(role.Name);
    }
}
