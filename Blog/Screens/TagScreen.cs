namespace Blog.Screens;

public static class TagScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem { Operation = Operations.Create, Title="Cadastrar Tag" },
            new MenuItem { Operation = Operations.Read, Title="Listar Tags" },
            new MenuItem { Operation = Operations.Update, Title="Atualizar Tag" },
            new MenuItem { Operation = Operations.Delete, Title="Excluir Tag" },
            new MenuItem { Operation = Operations.GoBack, Title="Voltar" },
            new MenuItem { Operation = Operations.Exit, Title="Sair" }
        };

        Screen.Create("Meu Blog - Gestão de Tags");

        var option = Menu.Create(menuItems);

        switch (option.Operation)
        {
            case Operations.Create:
                CreateTag();
                Load();
                break;

            case Operations.Read:
                ListTags();
                Load();
                break;

            case Operations.Update:
                UpdateTag();
                Load();
                break;

            case Operations.Delete:
                DeleteTag();
                Load();
                break;

            case Operations.Exit:
                Screen.Quit();
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

    private static void DeleteTag()
    {
        Write(new Rule("[yellow]Excluindo Tag[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );

        var id = Ask<int>("Qual id da tag para excluir?");
        var tag = FindTag(id);

        if (tag is null)
        {
            Message.Show("[yellow]O id informado não existe.[/]");
            return;
        }

        Write(new Table()
            .AddColumns("[grey]Título[/]", "[grey]Slug[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .AddRow(tag.Name, tag.Slug)
        );

        if (Confirm("Confirma exclusão da tag?"))
        {
            DeleteTag(id);
        }
    }

    private static void DeleteTag(int id)
    {
        try
        {
            var repository = new Repository<Tag>(Database.Connection);
            repository.Delete(id);
            Message.Show("[green]Tag excluída com sucesso. ✅[/]");
        }
        catch (Exception ex)
        {
            Message.Show($"[red]Não foi possível excluir a tag. {ex.Message} 😅[/]");
        }
    }

    private static Tag FindTag(int id)
    {
        var repository = new Repository<Tag>(Database.Connection);
        return repository.GetById(id);
    }

    private static void ListTags()
    {
        do
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
                    table.AddRow(tag.Id.ToString(), tag.Name, tag.Slug);
                }
            }

            Write(table);
        } while (System.Console.ReadKey().Key != ConsoleKey.Enter);
    }

    private static void UpdateTag()
    {
        Write(new Rule("[yellow]Atualizando Tag[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );

        var id = Ask<int>("Qual id da tag para atualizar?");

        if (FindTag(id) is null)
        {
            Message.Show("[yellow]O id informado não existe.[/]");
            return;
        }

        var name = Ask<string>("Título: ");
        var slug = Ask<string>("Slug: ");

        Write(new Table()
            .AddColumns("[grey]Título[/]", "[grey]Slug[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .AddRow(name, slug)
        );

        if (Confirm("Salvar dados da tag?"))
        {
            UpdateTag(new Tag { Id = id, Name = name, Slug = slug });
        }
    }

    private static void UpdateTag(Tag tag)
    {
        try
        {
            var repository = new Repository<Tag>(Database.Connection);
            repository.Update(tag);
            Message.Show("[green]Tag atualizada com sucesso. ✅[/]");
        }
        catch (Exception ex)
        {
            Message.Show($"[red]Não foi possível atualizar a tag. {ex.Message} 😅[/]");
        }
    }
}
