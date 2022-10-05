namespace Blog.Controls;

public class Screen
{
    public static void Create(string title)
    {
        ArgumentNullException.ThrowIfNull(title);
        Clear();

        Write(new Rule(title));
        WriteLine();
    }

    public static void Quit()
    {
        Clear();
        Message.Show("[blue]Até mais 👋.[/]");
        Environment.Exit(0);
    }
}
