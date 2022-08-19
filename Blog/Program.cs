using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

using var connection = new SqlConnection(connectionString);

if (!AnsiConsole.Profile.Capabilities.Interactive)
{
    MarkupLine("[red]Environment does not support interaction.[/]");
    return;
}

Write(new FigletText("Meu Blog").Centered().Color(Color.Purple_1));

Thread.Sleep(2000);

Clear();

MainScreen.Load();
