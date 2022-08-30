using Blog.Controls;

namespace Blog.Screens;

public static class TagScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem> {
                new MenuItem { Operation = Operations.ListTags, Title="Listar Tags" },
                new MenuItem { Operation = Operations.CreateTag, Title="Cadastrar Tag" },
                new MenuItem { Operation = Operations.GoBack, Title="Voltar" },
                new MenuItem { Operation = Operations.Exit, Title="Sair" }
            };

        Clear();

        Write(new Rule("Meu Blog - Gestão de Tags"));

        var option = MainScreen.AddMenu(menuItems);

        switch (option.Operation)
        {
            case Operations.ListTags:
                do
                {
                    ListTags();
                } while (System.Console.ReadKey().Key != ConsoleKey.Enter);
                Load();
                break;
            case Operations.CreateTag:
                CreateTag();
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

    private static void CreateTag()
    {
        Write(new Rule("[yellow]Nova Tag[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );

        var name = Ask<string>("Qual o título da Tag?");
        var slug = Ask<string>("Qual o slug da Tag?");

        Write(new Table()
            .AddColumns("[grey]Título[/]", "[grey]Slug[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .AddRow(name, slug)
        );

        if (Confirm("Salvar dados da tag?"))
        {
            CreateTag(new Tag { Name = name, Slug = slug });
        }
    }

    private static void CreateTag(Tag tag)
    {
        try
        {
            var repository = new Repository<Tag>(Database.Connection);
            repository.Create(tag);
            Message.Show("[green]Tag cadastrada com sucesso. ✅[/]");
        }
        catch (Exception ex)
        {
            Message.Show($"[red]Não foi possível cadastrar a tag. {ex.Message} 😅[/]");
        }
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
