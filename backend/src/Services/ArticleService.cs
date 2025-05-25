using Newtonsoft.Json;

public class ArticleService : IArticleService
{
    public async Task<List<Article>> GetArticlesAsync(HttpClient client, string path)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string articleResponse = await response.Content.ReadAsStringAsync();

                //Guard condition - prevents later null errors for articleDatasetFromString

                if (string.IsNullOrEmpty(articleResponse))
                {
                    Console.WriteLine("Empty response, no articles returned");
                    return new List<Article>();
                }

                ArticleDataset articleDatasetFromString = JsonConvert.DeserializeObject<ArticleDataset>(articleResponse);
                List<Article> articles = articleDatasetFromString.Articles ?? new List<Article>();
                return articles;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching articles: {ex.Message}");
            return new List<Article>();
        }
    }

    public async Task<List<ArticleDTO>> GetArticleTitles(HttpClient client, string path)
    {
        List<Article> articles = await GetArticlesAsync(client, path);
        List<ArticleDTO> articlesDTO = articles
        .Select(article => new ArticleDTO(article.Title, article.Url))
        .ToList();

        return articlesDTO;
    }
}