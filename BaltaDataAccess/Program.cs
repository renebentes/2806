using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

using var connection = new SqlConnection(connectionString);

CreateCategory(connection);
UpdateCategory(connection);
ListCategories(connection);

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM Category");

    foreach (var category in categories)
    {
        Console.WriteLine($"{category.Id} - {category.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
    var categoria = new Category()
    {
        Id = Guid.NewGuid(),
        Title = "Amazon AWS",
        Url = "amazon",
        Description = "Categoria destinada a serviços do AWS",
        Order = 8,
        Summary = "AWS Cloud",
        Featured = false
    };

    var insertSql = @"INSERT INTO
        [Category]
    VALUES (
        @Id,
        @Title,
        @Url,
        @Summary,
        @Order,
        @Description,
        @Featured)";

    connection.Execute(insertSql, new
    {
        categoria.Id,
        categoria.Title,
        categoria.Url,
        categoria.Summary,
        categoria.Order,
        categoria.Description,
        categoria.Featured
    });
}

static void UpdateCategory(SqlConnection connection)
{
    var updateSql = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";

    connection.Execute(updateSql, new
    {
        id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
        title = "Frontend 2022"
    });
}
