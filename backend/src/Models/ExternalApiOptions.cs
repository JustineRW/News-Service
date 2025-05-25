public class ExternalApiOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string HeadlinesPath { get; set; } = string.Empty;
    public string SearchPath { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;

    public int MAX_ARTICLE_NUMBER { get; set; } = 0;
}
