using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

using (var connection = new SqlConnection(connectionString))
{
    Console.WriteLine("Conectado");
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
