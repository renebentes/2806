using Blog.Controls;

namespace Blog.Screens;

public static class TagScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem> {
                new MenuItem { Operation = Operations.ListTags, Title="Listar Tags" },
                new MenuItem { Operation = Operations.GoBack, Title="Voltar" },
                new MenuItem { Operation = Operations.Exit, Title="Sair" }
            };

        Clear();

        Write(new Rule("Meu Blog - Gestão de Tags"));

        var option = Prompt(
            new SelectionPrompt<MenuItem>()
                .Title("O que você deseja fazer?")
                .AddChoices(menuItems)
                .UseConverter((menu) => menu.Title)
            );

        switch (option.Operation)
        {
            case Operations.ListTags:
                do
                {
                    ListTags();
                } while (System.Console.ReadKey().Key != ConsoleKey.Enter);
                Load();
                break;
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
        };
    }

    private static void ListTags()
    {
        var table = new Table()
            .Title("Listagem de Tags")
            .Caption("Pressione [[ [yellow]ENTER[/] ]] para voltar ao menu")
            .Centered()
            .Expand()
            .BorderColor(Color.Grey);

        var repository = new Repository<Tag>(Database.Connection);
        var tags = repository.GetAll();

        table.AddColumn("[yellow]Id[/]");
        table.AddColumn("[yellow]Name[/]");
        table.AddColumn("[yellow]Slug[/]");

        if (!tags.Any())
        {
            table.HideHeaders();
            table.AddRow("Nenhum registro encontrado!");
        }
        else
        {

            foreach (var tag in tags)
            {
                table.AddRow(tag.Id.ToString(), tag.Name.ToString(), tag.Slug.ToString());
            }
        }

        Write(table);
    }
}
