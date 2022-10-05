using Blog.Controls;

namespace Blog.Screens;

public static class RoleScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem { Operation = Operations.GoBack, Title = "Voltar" },
            new MenuItem { Operation = Operations.Exit, Title = "Sair" }
        };

        Clear();

        Write(new Rule("Meu Blog - Gestão de Perfis de Usuários"));
        WriteLine();

        var option = Menu.Create(menuItems);

        switch (option.Operation)
        {
            case Operations.Exit:
                MainScreen.Quit();
                break;

            case Operations.GoBack:
                MainScreen.Load();
                break;

            default:
                Message.Show("[red]Opção inválida 😅.[/]");
                Load();
                break;
        }
    }
}
