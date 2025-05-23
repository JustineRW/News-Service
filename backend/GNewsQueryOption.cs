public class GNewsQueryOptions(string query = "")
{
    string Country = "au";
    string Language = "en";

    string ApiKey = GNewsApiKey.Key;

    int MaxNumberOfArticles = 10;

    string Category = "science";

    string Query = query;

    public string GetSearchQueryOptions()
    {
        return $"?q={Query}&lang={Language}&country={Country}&max={MaxNumberOfArticles}&apikey={ApiKey}";
    }
    public string GetTopHeadlineQueryOptions()
    {
        return $"?category={Category}&lang={Language}&country={Country}&max={MaxNumberOfArticles}&apikey={ApiKey}";
    }
}