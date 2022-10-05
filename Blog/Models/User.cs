using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(User)}]")]
public class User : ModelBase
{
    public string Bio { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    [Write(false)]
    public List<Role> Roles { get; set; } = new();

    public string Slug { get; set; } = string.Empty;
}
