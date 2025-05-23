using Newtonsoft.Json;

public class SearchService
{
    public async Task<List<Article>> SearchArticlesAsync(HttpClient client, string path, int numberOfArticles = 10)
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
}