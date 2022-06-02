using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

using var connection = new SqlConnection(connectionString);

// CreateUser(connection);
ReadUsers(connection);
UpdateUser(connection);
ReadUser(connection);
DeleteUser(connection);

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

    connection.Insert<User>(user);
    Console.WriteLine("Usuário criado com sucesso!");
}

static void ReadUsers(SqlConnection connection)
{
    var users = connection.GetAll<User>();

    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
}

static void ReadUser(SqlConnection connection)
{
    var user = connection.Get<User>(1);
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

    connection.Update<User>(user);
    Console.WriteLine("Usuário atualizado com sucesso!");
}

static void DeleteUser(SqlConnection connection)
{
    var user = connection.Get<User>(1);
    connection.Delete<User>(user);
    Console.WriteLine("Usuário apagado com sucesso!");
}
