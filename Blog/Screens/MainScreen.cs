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

        Screen.Create("Meu Blog");

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
                Screen.Quit();
                break;

            default:
                Message.Show("[red]Opção inválida 😅.[/]");
                Load();
                break;
        };
    }
}
