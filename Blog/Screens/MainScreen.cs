using Blog.Controls;

namespace Blog.Screens;

public static class MainScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem { Operation = Operations.UserManager, Title="Gestão de Usuários" },
            new MenuItem { Operation = Operations.RoleManager, Title="Gestão de Perfis de Usuários" },
            new MenuItem { Operation = Operations.TagManager, Title="Gestão de Tags" },
            new MenuItem { Operation = Operations.Exit, Title="Sair" }
        };

        Clear();

        Write(new Rule("Meu Blog"));
        WriteLine();

        var option = Menu.Create(menuItems);

        switch (option.Operation)
        {
            case Operations.UserManager:
                UserScreen.Load();
                break;

            case Operations.RoleManager:
                RoleScreen.Load();
                break;

            case Operations.TagManager:
                TagScreen.Load();
                break;

            case Operations.Exit:
                Quit();
                break;

            default:
                Message.Show("[red]Opção inválida 😅.[/]");
                Load();
                break;
        };
    }

    public static void Quit()
    {
        Clear();
        Message.Show("[blue]Até mais 👋.[/]");
        Environment.Exit(0);
    }
}
