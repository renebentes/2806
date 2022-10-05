namespace Blog.Screens;

public static class RoleScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem { Operation = Operations.Create, Title = "Cadastrar Perfil" },
            new MenuItem { Operation = Operations.Read, Title="Listar Perfis" },
            new MenuItem { Operation = Operations.GoBack, Title = "Voltar" },
            new MenuItem { Operation = Operations.Exit, Title = "Sair" }
        };

        Screen.Create("Meu Blog - Gestão de Perfis de Usuários");

        var option = Menu.Create(menuItems);

        switch (option.Operation)
        {
            case Operations.Create:
                CreateRole();
                Load();
                break;

            case Operations.Read:
                ListRoles();
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
        }
    }

    private static void CreateRole()
    {
        Write(new Rule("[yellow]Novo Perfil[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );

        var name = Ask<string>("Nome:");
        var slug = Ask<string>("Slug:");

        Write(new Table()
            .AddColumns("[grey]Nome[/]", "[grey]Slug[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .AddRow(name, slug)
        );

        if (Confirm("Salvar dados do perfil?"))
        {
            CreateRole(new Role
            {
                Name = name,
                Slug = slug
            });
        }
    }

    private static void CreateRole(Role role)
    {
        try
        {
            var repository = new Repository<Role>(Database.Connection);
            repository.Create(role);
            Message.Show("[green]Perfil de usuário cadastrado com sucesso. ✅[/]");
        }
        catch (Exception ex)
        {
            Message.Show($"[red]Não foi possível cadastrar o perfil de usuário. {ex.Message} 😅[/]");
        }
    }

    private static void ListRoles()
    {
        do
        {
            var table = new Table()
                .Title("Listagem de Perfis")
                .Caption("Pressione [[ [yellow]ENTER[/] ]] para voltar ao menu")
                .Centered()
                .Expand()
                .BorderColor(Color.Grey);

            var repository = new Repository<Role>(Database.Connection);
            var roles = repository.GetAll();

            table.AddColumn("[yellow]Id[/]");
            table.AddColumn("[yellow]Nome[/]");
            table.AddColumn("[yellow]Slug[/]");

            if (!roles.Any())
            {
                table.HideHeaders();
                table.AddRow("Nenhum registro encontrado!");
            }
            else
            {
                foreach (var role in roles)
                {
                    table.AddRow(role.Id.ToString(), role.Name, role.Slug);
                }
            }

            Write(table);
        } while (System.Console.ReadKey().Key != ConsoleKey.Enter);
    }
}
