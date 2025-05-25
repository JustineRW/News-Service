using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;


namespace backend.Tests;

public class ArticlesControllerTests
{
    private Mock<IArticleService> mockArticleService;
    private IOptions<ExternalApiOptions> apiOptions;
    private ArticlesController controller;

    public ArticlesControllerTests()
    {
        mockArticleService = new Mock<IArticleService>();
        HttpClient httpClient = new HttpClient();

        ExternalApiOptions testApiOptions = new ExternalApiOptions
        {
            BaseUrl = "https://test-api.com",
            SearchPath = "/search?q=",
            HeadlinesPath = "/headlines?",
            MAX_ARTICLE_NUMBER = 10
        };

        apiOptions = Options.Create(testApiOptions);

        // Create controller with mocked dependencies
        controller = new ArticlesController(apiOptions, httpClient, mockArticleService.Object);
    }

    [Fact]
    public async Task GetArticles_WithSearchKeywords_ReturnsSearchResults()
    {

        // Arrange
        var queryOptions = new GNewsQueryOptions
        {
            SearchKeywords = "cake",
            NumberOfArticles = 5
        };

        var expectedArticles = new List<Article>
            {
                new Article { Title = "Baking News 1", Url = "http://example.com/1" },
                new Article { Title = "Baking News 2", Url = "http://example.com/2" }
            };

        mockArticleService
            .Setup(s => s.GetArticlesAsync(It.IsAny<HttpClient>(), It.IsAny<string>()))
            .ReturnsAsync(expectedArticles);

        // Act
        var result = await controller.GetArticles(queryOptions);

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<News>>>(result);
        var articles = Assert.IsAssignableFrom<IEnumerable<News>>(actionResult.Value);
        Assert.Equal(2, articles.Count());

        // Verify the service was called
        mockArticleService.Verify(
            s => s.GetArticlesAsync(It.IsAny<HttpClient>(), It.IsAny<string>()),
            Times.Once);
    }
}
