using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(Role)}]")]
public class Role : ModelBase
{
    public string Name { get; set; }

    public string Slug { get; set; }
}
