using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(User)}]")]
public class User : ModelBase
{
    public User()
        => Roles = new();

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Bio { get; set; }

    public string Image { get; set; }

    public string Slug { get; set; }

    [Write(false)]
    public List<Role> Roles { get; set; }
}
