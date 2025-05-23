
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

        [HttpGet]
        public async Task<ActionResult<List<string>>> Get()
        {

            client.BaseAddress = new Uri("https://gnews.io/api/v4/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            List<string> articleTitles = new();

            try
            {
                //TODO
                // handle params
                // handle key in a more secure way
                List<Article> articles = await articleFetchingService.GetArticlesAsync(client, $"top-headlines?category=general&lang=en&country=au&max=10&apikey={GNewsApiKey.Key}");
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




