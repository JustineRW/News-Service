
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace ArticleController
{
    [ApiController]
    [Route("api/articles")]
    public class ArticlesController : ControllerBase
    {

        static HttpClient client = new HttpClient();
        static ArticleService articleFetchingService = new ArticleService();
        static GNewsQueryOptions queryOptions = new GNewsQueryOptions();

        [HttpGet]
        public async Task<ActionResult<List<string>>> Get()
        {

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://gnews.io/api/v4/");
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            List<string> searchResults = new();
            List<string> articleTitles = new();


            if (!string.IsNullOrEmpty(Request.Query["search"]))
            {
                string searchQuery = Request.Query["search"];
                GNewsQueryOptions searchQueryOptions = new GNewsQueryOptions(searchQuery);
                searchResults = await SearchArticles(searchQueryOptions);

            }
            else
            {
                articleTitles = await GetTopHeadlines();
            }

            return searchResults;
        }

        private static async Task<List<string>> GetTopHeadlines()
        {

            List<string> articleTitles = new();
            try
            {
                //TODO
                // handle key in a more secure way

                List<Article> articles = await articleFetchingService.GetArticlesAsync(client, "top-headlines" + queryOptions.GetTopHeadlineQueryOptions());
                articleTitles = articleFetchingService.GetArticleTitles(articles);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return articleTitles;
        }
        private static async Task<List<string>> SearchArticles(GNewsQueryOptions searchQueryOptions)
        {
            List<string> articleTitles = new();
            try
            {
                //TODO
                // handle key in a more secure way

                List<Article> articles = await articleFetchingService.GetArticlesAsync(client, "search" + searchQueryOptions.GetSearchQueryOptions());
                articleTitles = articleFetchingService.GetArticleTitles(articles);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return articleTitles;
        }
    }

}




