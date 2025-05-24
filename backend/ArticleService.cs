using Newtonsoft.Json;

public class ArticleService
{
    public async Task<List<Article>> GetArticlesAsync(HttpClient client, string path)
    {
        HttpResponseMessage response = await client.GetAsync(path);

        if (response.IsSuccessStatusCode)
        {
            string articleResponse = await response.Content.ReadAsStringAsync();
            ArticleDataset articleDatasetFromString = JsonConvert.DeserializeObject<ArticleDataset>(articleResponse);
            List<Article> articles = articleDatasetFromString?.Articles ?? new List<Article>();
            return articles;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    public async Task<List<string>> GetArticleTitles(HttpClient client, string path)
    {
        List<Article> articles = await GetArticlesAsync(client, path);
        return articles.Select(article => article.Title).ToList();
    }
}