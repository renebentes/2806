using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(User)}]")]
public class User : ModelBase
{
    private readonly IList<Role> _roles;

    public User()
        => _roles = new List<Role>();

    public string Bio { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    [Write(false)]
    public IReadOnlyList<Role> Roles => _roles.ToList();

    public string Slug { get; set; } = string.Empty;

    public void AddRole(Role role)
    {
        if (!_roles.Any(r => r.Id == role.Id))
        {
            _roles.Add(role);
        }
    }

    public void AddRoles(IEnumerable<Role> roles)
    {
        foreach (var role in roles)
        {
            AddRole(role);
        }
    }
}
