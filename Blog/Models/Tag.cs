using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(Tag)}]")]
public class Tag : ModelBase
{
    public string Name { get; set; }

    public string Slug { get; set; }
}
