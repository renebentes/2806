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
            new MenuItem { Operation = Operations.Update, Title="Atualizar Usuário" },
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

            case Operations.Update:
                UpdateUser();
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

        var name = AskName();
        var email = AskEmail();
        var password = AskPassword();
        var bio = AskBio();
        var image = AskImage();
        var slug = AskSlug();
        var selectedRoles = SelectRoles();

        ShowPreview(name, email, bio, image, slug, selectedRoles);

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

    private static string AskBio()
        => Prompt(new TextPrompt<string>("Biografia:")
            .AllowEmpty());

    private static string AskEmail()
        => Ask<string>("E-mail:");

    private static int AskId()
        => Ask<int>("Id:");

    private static string AskImage()
        => Prompt(new TextPrompt<string>("Foto:")
            .AllowEmpty());

    private static string AskName()
        => Ask<string>("Nome:");

    private static string AskPassword()
        => Prompt(new TextPrompt<string>("Senha:")
            .Secret());

    private static string AskSlug()
        => Ask<string>("Slug:");

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

    private static IEnumerable<Role> SelectRoles()
    {
        var repository = new Repository<Role>(Database.Connection);
        var roles = repository.GetAll();

        return Prompt(new MultiSelectionPrompt<Role>()
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
    }

    private static void ShowPreview(string name,
                                    string email,
                                    string bio,
                                    string image,
                                    string slug,
                                    IEnumerable<Role> roles)
        => Write(new Table()
            .AddColumns("[grey]Nome[/]",
                        "[grey]E-mail[/]",
                        "[grey]Biografia[/]",
                        "[grey]Foto[/]",
                        "[grey]Slug[/]",
                        "[grey]Perfis[/]")
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .AddRow(name,
                    email,
                    bio,
                    image,
                    slug,
                    string.Join(",", selectedRoles.Select(role => role.Name)))
        );

    private static void UpdateUser()
    {
        Write(new Rule("[yellow]Atualizando Usuário[/]")
            .RuleStyle("grey")
            .LeftAligned()
        );

        var id = AskId();
        var repository = new UserRepository(Database.Connection);
        var user = repository.GetByIdWithRoles(id);

        if (user is null)
        {
            Message.Show("[yellow]Usuário não encontrado.[/]");
            return;
        }

        ShowPreview(user.Name, user.Email, user.Bio, user.Image, user.Slug, user.Roles);

        var name = AskName();
        var email = AskEmail();
        var password = AskPassword();
        var bio = AskBio();
        var image = AskImage();
        var slug = AskSlug();
        var selectedRoles = SelectRoles();

        ShowPreview(name, email, bio, image, slug, selectedRoles);

        if (Confirm("Salvar dados do usuário?"))
        {

            user.Name = name;
            user.Email = email;
            user.PasswordHash = password.ToSha256();
            user.Bio = bio;
            user.Image = image;
            user.Slug = slug;

            user.Roles.AddRange(selectedRoles);
            UpdateUser(user);
        }
    }

    private static void UpdateUser(User user)
    {
        try
        {
            var repository = new UserRepository(Database.Connection);
            repository.Update(user);
            Message.Show("[green]Usuário atualizado com sucesso. ✅[/]");
        }
        catch (Exception ex)
        {
            Message.Show($"[red]Não foi possível atualizar o usuário. {ex.Message} 😅[/]");
        }
    }
}
