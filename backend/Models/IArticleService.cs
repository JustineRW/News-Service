public interface IArticleService
{
    public Task<List<Article>> GetArticlesAsync(HttpClient client, string path);
    public Task<List<ArticleDTO>> GetArticleTitles(HttpClient client, string path);
}
