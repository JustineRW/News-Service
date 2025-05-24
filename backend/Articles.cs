

public interface News
{
    string Title { get; set; }
    string Url { get; set; }
}

public class ArticleDataset
{
    public int TotalArticles { get; set; } = 0;
    public List<Article> Articles { get; set; } = new();
}

public class Article() : News
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public NewsSource Source { get; set; } = new();
}

public class ArticleDTO : News
{
    public ArticleDTO(string title, string url)
    {
        Title = title;
        Url = url;
    }

    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class NewsSource()
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

