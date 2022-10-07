using Blog.Extensions;

namespace Blog.Screens;

public static class UserScreen
{
    public static void Load()
    {
        IReadOnlyList<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem { Operation = Operations.Create, Title="Cadastrar Usuário" },
            new MenuItem { Operation = Operations.Read, Title="Listar Usuários" },
            new MenuItem { Operation = Operations.GoBack, Title="Voltar" },
            new MenuItem { Operation = Operations.Exit, Title="Sair" }
        };

        Screen.Create("Meu Blog - Gestão de Usuários");

        var option = Menu.Create(menuItems);

        switch (option.Operation)
        {
            case Operations.Create:
                CreateUser();
                Load();
                break;

            case Operations.Read:
                ListUsers();
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

    private static void CreateUser()
    {
        Write(new Rule("[yellow]Novo Usuário[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );

        var name = Ask<string>("Nome:");
        var email = Ask<string>("E-mail:");
        var password = Prompt(new TextPrompt<string>("Senha:")
                    .Secret());
        var bio = Prompt(new TextPrompt<string>("Biografia:")
                    .AllowEmpty());
        var image = Prompt(new TextPrompt<string>("Foto:")
                    .AllowEmpty());
        var slug = Ask<string>("Slug:");

        var repository = new Repository<Role>(Database.Connection);
        var roles = repository.GetAll();
        var selectedRoles = Prompt(
            new MultiSelectionPrompt<Role>()
                .Title("Perfil de Usuário:")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Mova para cima e para baixo para navevar entre as páginas)[/]")
                .InstructionsText(
                    @"[grey](Pressione [blue]<espaço>[/] para selecionar um perfil, " +
                    "[green]<enter>[/] para confirmar)[/]")
                .AddChoices(roles)
                .UseConverter(role => role.Name)
                );

        Write(new Table()
            .AddColumns("[grey]Nome[/]", "[grey]E-mail[/]", "[grey]Biografia[/]", "[grey]Foto[/]", "[grey]Slug[/]", "[grey]Perfis[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .AddRow(name,
                    email,
                    bio,
                    image,
                    slug,
                    string.Join(",", selectedRoles.Select(role => role.Name)))
        );

        if (Confirm("Salvar dados do usuário?"))
        {

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = password.ToSha256(),
                Bio = bio,
                Image = image,
                Slug = slug
            };

            user.Roles.AddRange(selectedRoles);
            CreateUser(user);
        }
    }

    private static void CreateUser(User user)
    {
        try
        {
            var repository = new UserRepository(Database.Connection);
            repository.Create(user);
            Message.Show("[green]Usuário cadastrado com sucesso. ✅[/]");
        }
        catch (Exception ex)
        {
            Message.Show($"[red]Não foi possível cadastrar o usuário. {ex.Message} 😅[/]");
        }
    }

    private static void ListUsers()
    {
        do
        {
            var table = new Table()
                .Title("Listagem de Usuários")
                .Caption("Pressione [[ [yellow]ENTER[/] ]] para voltar ao menu")
                .Centered()
                .Expand()
                .BorderColor(Color.Grey);

            var repository = new UserRepository(Database.Connection);
            var users = repository.GetAllWithRoles();

            table.AddColumn("[yellow]Id[/]");
            table.AddColumn("[yellow]Nome[/]");
            table.AddColumn("[yellow]E-mail[/]");
            table.AddColumn("[yellow]Slug[/]");
            table.AddColumn("[yellow]Perfis[/]");

            if (!users.Any())
            {
                table.HideHeaders();
                table.AddRow("Nenhum registro encontrado!");
            }
            else
            {
                foreach (var user in users)
                {
                    table.AddRow(
                        user.Id.ToString(),
                        user.Name,
                        user.Email,
                        user.Slug,
                        string.Join(",", user.Roles.Select(role => role.Name)
                    ));
                }
            }

            Write(table);
        } while (System.Console.ReadKey().Key != ConsoleKey.Enter);
    }
}
