using Blog.Controls;

namespace Blog.Screens;

public static class RoleScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem { Operation = Operations.CreateRole, Title = "Cadastrar Perfil" },
            new MenuItem { Operation = Operations.GoBack, Title = "Voltar" },
            new MenuItem { Operation = Operations.Exit, Title = "Sair" }
        };

        Clear();

        Write(new Rule("Meu Blog - Gestão de Perfis de Usuários"));
        WriteLine();

        var option = Menu.Create(menuItems);

        switch (option.Operation)
        {
            case Operations.CreateRole:
                CreateRole();
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
}
