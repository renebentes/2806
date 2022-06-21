using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(Tag)}]")]
public class Tag
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Slug { get; set; }
}
