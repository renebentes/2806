using Blog;
using Blog.Controls;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

if (!AnsiConsole.Profile.Capabilities.Interactive)
{
    Message.Show("[red]Environment does not support interaction.[/]");
    return;
}

Write(new Panel(
        new FigletText("Meu Blog")
        .Centered()
        .Color(Color.Purple_1))
    .Expand());

Thread.Sleep(2000);

Clear();

Database.Connection = new SqlConnection(connectionString);
Database.Connection.Open();

MainScreen.Load();

Database.Connection.Close();
