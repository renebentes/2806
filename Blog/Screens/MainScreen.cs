using Blog.Controls;

namespace Blog.Screens;

public static class MainScreen
{
    public static MenuItem AddMenu(IReadOnlyList<MenuItem> menuItems)
        => Prompt(new SelectionPrompt<MenuItem>()
                .Title("O que você deseja fazer?")
                .AddChoices(menuItems)
                .UseConverter((menu) => menu.Title));

    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem> {
            new MenuItem { Operation = Operations.UserManager, Title="Gestão de Usuários" },
            new MenuItem { Operation = Operations.TagManager, Title="Gestão de Tags" },
            new MenuItem { Operation = Operations.Exit, Title="Sair" }
        };

        Clear();

        Write(new Rule("Meu Blog"));

        var option = AddMenu(menuItems);

        switch (option.Operation)
        {
            case Operations.UserManager:
                UserScreen.Load();
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
