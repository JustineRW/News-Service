
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
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

        if (string.IsNullOrEmpty(this.apiOptions.ApiKey))
        {
            throw new InvalidOperationException("ApiKey is not configured. Please set it using dotnet user-secrets");
        }
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

        string cleanedKeywords = Regex.Replace(
            Regex.Replace(gNewsQueryOptions?.SearchKeywords.Trim(), @"[^\w\s\-'""]", ""), @"\s+", " "
        );

        gNewsQueryOptions.SearchKeywords = cleanedKeywords;

    }

    private async Task<List<Article>> SearchArticles(QueryOptions queryOptions)
    {
        string path = apiOptions.SearchPath + queryOptions.GetSearchQuery() + $"&apikey={apiOptions.ApiKey}";
        List<Article> articles = await articleService.GetArticlesAsync(httpClient, path);
        return articles;
    }

    private async Task<List<ArticleDTO>> GetTopHeadlines(QueryOptions queryOptions)
    {
        string path = apiOptions.HeadlinesPath + queryOptions.GetTopHeadlineQueryOptions() + $"&apikey={apiOptions.ApiKey}";
        List<ArticleDTO> articleTitles = await articleService.GetArticleTitles(httpClient, path);
        return articleTitles;
    }
}







