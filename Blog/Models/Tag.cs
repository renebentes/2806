using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(Tag)}]")]
public class Tag : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;
}
