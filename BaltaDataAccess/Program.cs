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
ReadView(connection);
OneToOne(connection);
OneToMany(connection);
QueryMultiple(connection);
SelectIn(connection);
Like(connection, "api");

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

static void ReadView(SqlConnection connection)
{
    var sql = "SELECT * FROM vwCourses";
    var courses = connection.Query(sql);

    foreach (var item in courses)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void OneToOne(SqlConnection connection)
{
    var sql = @"
    SELECT * FROM [CareerItem]
    INNER JOIN [Course] ON [CareerItem].[CourseId] = [Course].[Id]";
    var items = connection.Query<CareerItem, Course, CareerItem>(sql, (careerItem, course) =>
    {
        careerItem.Course = course;
        return careerItem;
    }, splitOn: "Id");

    foreach (var item in items)
    {
        Console.WriteLine($"{item.Title} - Curso: {item.Course.Title}");
    }
}

static void OneToMany(SqlConnection connection)
{
    var sql = @"
    SELECT
        Career.Id,
        Career.Title,
        CareerItem.CareerId,
        CareerItem.Title
    FROM
        [Career]
    INNER JOIN
        [CareerItem] ON [Career].[Id] = [CareerItem].[CareerId]
    ORDER BY
        [Career.Title]";

    var careers = new List<Career>();
    var items = connection.Query<Career, CareerItem, Career>(sql, (career, item) =>
    {
        var car = careers.FirstOrDefault(x => x.Id == career.Id);

        if (car is null)
        {
            car = career;
            car.Items.Add(item);
            careers.Add(car);
        }
        else
        {
            car.Items.Add(item);
        }

        return career;
    }, splitOn: "CareerId");

    foreach (var career in careers)
    {
        Console.WriteLine($"{career.Title}");
        foreach (var item in career.Items)
        {
            Console.WriteLine($" - {item.Title}");
        }
    }
}

static void QueryMultiple(SqlConnection connection)
{
    var query = "SELECT * FROM [Category]; SELECt * FROM [Course];";
    using var multi = connection.QueryMultiple(query);
    var categories = multi.Read<Category>();
    var courses = multi.Read<Course>();

    foreach (var item in categories)
    {
        Console.WriteLine(item.Title);
    }

    foreach (var item in courses)
    {
        Console.WriteLine(item.Title);
    }
}

static void SelectIn(SqlConnection connection)
{
    var query = "SELECT * FROM [Career] WHERE [Id] IN @Id;";
    var items = connection.Query<Career>(query, new
    {
        Id = new[]{
            "4327ac7e-963b-4893-9f31-9a3b28a4e72b",
                    "e6730d1c-6870-4df3-ae68-438624e04c72"
        }
    });

    foreach (var item in items)
    {
        Console.WriteLine(item.Title);
    }
}

static void Like(SqlConnection connection, string term)
{
    var query = "SELECT * FROM [Course] WHERE [Title] @exp";
    var items = connection.Query<Course>(query, new
    {
        exp = $"%{term}%"
    });

    foreach (var item in items)
    {
        Console.WriteLine(item.Title);
    }
}
