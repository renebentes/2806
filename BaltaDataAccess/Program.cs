using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

using var connection = new SqlConnection(connectionString);

CreateCategory(connection);
CreateManyCategories(connection);
UpdateCategory(connection);
ListCategories(connection);
ExecuteProcedure(connection);
ExecuteReadProcedure(connection);
ExecuteScalar(connection);

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

static void CreateManyCategories(SqlConnection connection)
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

    var categoria2 = new Category()
    {
        Id = Guid.NewGuid(),
        Title = "Nova Categoria",
        Url = "nova-categoria",
        Description = "Nova Categoria",
        Order = 9,
        Summary = "Categoria",
        Featured = true
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

    connection.Execute(insertSql, new[]
    {
        new
        {
            categoria.Id,
            categoria.Title,
            categoria.Url,
            categoria.Summary,
            categoria.Order,
            categoria.Description,
            categoria.Featured
        },
        new
        {
            categoria2.Id,
            categoria2.Title,
            categoria2.Url,
            categoria2.Summary,
            categoria2.Order,
            categoria2.Description,
            categoria2.Featured
        }
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

static void ExecuteProcedure(SqlConnection connection)
{
    var procedure = "spDeleteStudent";
    var parameters = new { StudentId = "2d4108c0-af71-411f-b0fb-5c98248686c2" };

    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
}

static void ExecuteReadProcedure(SqlConnection connection)
{
    var procedure = "spGetCoursesByCategory";
    var parameters = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
    var courses = connection.Query(procedure, parameters, commandType: CommandType.StoredProcedure);

    foreach (var course in courses)
    {
        Console.WriteLine(course.Title);
    }
}

static void ExecuteScalar(SqlConnection connection)
{
    var categoria = new Category()
    {
        Title = "Amazon AWS",
        Url = "amazon",
        Description = "Categoria destinada a serviços do AWS",
        Order = 8,
        Summary = "AWS Cloud",
        Featured = false
    };

    var insertSql = @"
    INSERT INTO
        [Category]
    OUTPUT inserted.[Id]
    VALUES (
        NEWID(),
        @Title,
        @Url,
        @Summary,
        @Order,
        @Description,
        @Featured)";

    var id = connection.ExecuteScalar<Guid>(insertSql, new
    {
        categoria.Title,
        categoria.Url,
        categoria.Summary,
        categoria.Order,
        categoria.Description,
        categoria.Featured
    });
    System.Console.WriteLine($"Categoria id: {id} inserida.");
}
