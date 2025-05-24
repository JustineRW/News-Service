
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
        private readonly ExternalApiOptions _apiOptions;
        public ArticlesController(IOptions<ExternalApiOptions> apiOptions)
        {
            _apiOptions = apiOptions.Value;
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetArticles([FromQuery] GNewsQueryOptions gNewsQueryOptions)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_apiOptions.BaseUrl);
            }
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Allow only the max article number
            if (gNewsQueryOptions.NumberOfArticles > _apiOptions.MAX_ARTICLE_NUMBER)
            {
                gNewsQueryOptions.NumberOfArticles = _apiOptions.MAX_ARTICLE_NUMBER;
            }

            // If the call has a search query, fetch from the /search endpoint
            Console.WriteLine("search keywords:" + gNewsQueryOptions.SearchKeywords);
            if (!string.IsNullOrEmpty(gNewsQueryOptions.SearchKeywords))
            {
                List<string> searchResults = await SearchArticles(gNewsQueryOptions);
                return searchResults;
            }

            List<string> articleTitles = await GetTopHeadlines(gNewsQueryOptions);
            return articleTitles;

        }

        private async Task<List<string>> SearchArticles(QueryOptions queryOptions)
        {
            string path = _apiOptions.SearchPath + queryOptions.GetSearchQuery();
            List<string> articleTitles = await GetArticleTitles(path);
            return articleTitles;
        }
        private async Task<List<string>> GetTopHeadlines(QueryOptions queryOptions)
        {
            string path = _apiOptions.HeadlinesPath + queryOptions.GetTopHeadlineQueryOptions();
            List<string> articleTitles = await GetArticleTitles(path);
            return articleTitles;
        }
        private async Task<List<string>> GetArticleTitles(string path)
        {
            List<string> articleTitles = new();
            try
            {
                path += $"&apikey={GNewsApiKey.Key}";
                Console.WriteLine("Get: " + path);
                articleTitles = await articleFetchingService.GetArticleTitles(client, path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return articleTitles;
        }
    }

}




