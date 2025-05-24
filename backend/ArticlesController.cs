
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ArticleController
{
    [ApiController]
    [Route("api/articles")]
    public class ArticlesController : ControllerBase
    {
        static HttpClient client = new HttpClient();
        static ArticleService articleFetchingService = new ArticleService();
        private const int MAX_QUERY_LENGTH = 100;
        private readonly ExternalApiOptions _apiOptions;
        public ArticlesController(IOptions<ExternalApiOptions> apiOptions)
        {
            _apiOptions = apiOptions.Value;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetArticles([FromQuery] GNewsQueryOptions gNewsQueryOptions)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_apiOptions.BaseUrl);
            }
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            SantiseUserInput(gNewsQueryOptions);

            // If the call has a search query, fetch from the /search endpoint
            if (!string.IsNullOrEmpty(gNewsQueryOptions.SearchKeywords))
            {
                Console.WriteLine("search keywords:" + gNewsQueryOptions.SearchKeywords);
                List<Article> searchResults = await SearchArticles(gNewsQueryOptions);
                return searchResults;
            }

            List<ArticleDTO> articleTitles = await GetTopHeadlines(gNewsQueryOptions);
            return articleTitles;

        }

        private void SantiseUserInput(GNewsQueryOptions gNewsQueryOptions)
        {
            // Allow only the max article number
            if (gNewsQueryOptions.NumberOfArticles > _apiOptions.MAX_ARTICLE_NUMBER)
            {
                gNewsQueryOptions.NumberOfArticles = _apiOptions.MAX_ARTICLE_NUMBER;
            }

            if (!string.IsNullOrEmpty(gNewsQueryOptions.SearchKeywords) && gNewsQueryOptions.SearchKeywords.Length > MAX_QUERY_LENGTH)
            {
                gNewsQueryOptions.SearchKeywords = gNewsQueryOptions.SearchKeywords.Substring(0, MAX_QUERY_LENGTH);
            }
        }

        private async Task<List<Article>> SearchArticles(QueryOptions queryOptions)
        {
            string path = _apiOptions.SearchPath + queryOptions.GetSearchQuery();
            List<Article> articleTitles = await GetArticles(path);
            return articleTitles;
        }

        private async Task<List<ArticleDTO>> GetTopHeadlines(QueryOptions queryOptions)
        {
            string path = _apiOptions.HeadlinesPath + queryOptions.GetTopHeadlineQueryOptions();
            List<ArticleDTO> articleTitles = await GetArticleTitles(path);
            return articleTitles;
        }

        private async Task<List<ArticleDTO>> GetArticleTitles(string path)
        {
            List<ArticleDTO> articleTitles = new();
            try
            {
                path += $"&apikey={GNewsApiKey.Key}";
                articleTitles = await articleFetchingService.GetArticleTitles(client, path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return articleTitles;
        }
        private async Task<List<Article>> GetArticles(string path)
        {
            List<Article> articles = new();
            try
            {
                path += $"&apikey={GNewsApiKey.Key}";
                articles = await articleFetchingService.GetArticlesAsync(client, path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return articles;
        }
    }

}




