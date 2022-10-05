using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(Category)}]")]
public class Category
{
    public string Body { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }

    public int Id { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public string Slug { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
