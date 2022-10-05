using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(Role)}]")]
public class Role : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;
}
