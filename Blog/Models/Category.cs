using Dapper.Contrib.Extensions;

namespace Blog.Models;

[Table($"[{nameof(Category)}]")]
public class Category
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public string Body { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public string Slug { get; set; }
}
