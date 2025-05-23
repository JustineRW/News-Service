
public class ArticleDataset
{
    public int TotalArticles;

    public List<Article> Articles;
}

public class Article()
{
    public string Title;
    public string Description;
    public string Url;
    public string Image;
    public DateTime PublishedAt;
    public NewsSource Source;
}

public class NewsSource()
{
    public string Name;
    public string Url;
}
