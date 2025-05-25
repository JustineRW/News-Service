
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


[ApiController]
[Route("api/articles")]
public class ArticlesController : ControllerBase
{
    private readonly HttpClient httpClient;
    private readonly IArticleService articleService;
    private readonly ExternalApiOptions apiOptions;
    private const int MAX_QUERY_LENGTH = 100;
    public ArticlesController(
         IOptions<ExternalApiOptions> apiOptions,
         HttpClient httpClient,
         IArticleService articleService)
    {
        this.apiOptions = apiOptions.Value;
        this.httpClient = httpClient;
        this.articleService = articleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<News>>> GetArticles([FromQuery] GNewsQueryOptions gNewsQueryOptions)
    {
        if (httpClient.BaseAddress == null)
        {
            httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
        }
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(
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
        if (gNewsQueryOptions.NumberOfArticles > apiOptions.MAX_ARTICLE_NUMBER)
        {
            gNewsQueryOptions.NumberOfArticles = apiOptions.MAX_ARTICLE_NUMBER;
        }

        if (!string.IsNullOrEmpty(gNewsQueryOptions.SearchKeywords) && gNewsQueryOptions.SearchKeywords.Length > MAX_QUERY_LENGTH)
        {
            gNewsQueryOptions.SearchKeywords = gNewsQueryOptions.SearchKeywords.Substring(0, MAX_QUERY_LENGTH);
        }
    }

    private async Task<List<Article>> SearchArticles(QueryOptions queryOptions)
    {
        string path = apiOptions.SearchPath + queryOptions.GetSearchQuery();
        List<Article> articleTitles = await GetArticles(path);
        return articleTitles;
    }

    private async Task<List<ArticleDTO>> GetTopHeadlines(QueryOptions queryOptions)
    {
        string path = apiOptions.HeadlinesPath + queryOptions.GetTopHeadlineQueryOptions();
        List<ArticleDTO> articleTitles = await GetArticleTitles(path);
        return articleTitles;
    }

    private async Task<List<ArticleDTO>> GetArticleTitles(string path)
    {
        List<ArticleDTO> articleTitles = new();
        try
        {
            path += $"&apikey={GNewsApiKey.Key}";
            articleTitles = await articleService.GetArticleTitles(httpClient, path);
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
            articles = await articleService.GetArticlesAsync(httpClient, path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return articles;
    }
}







