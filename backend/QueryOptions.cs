using Microsoft.AspNetCore.Mvc;

public abstract class QueryOptions
{
    public string? Language { get; set; }
    public string? Category { get; set; }
    public string? Country { get; set; }

    [FromQuery(Name = "pagesize")]
    public int? NumberOfArticles { get; set; }

    [FromQuery(Name = "search")]
    public string? SearchKeywords { get; set; }

    protected Dictionary<string, string> ParameterNames = new();

    protected abstract void SetParameterDictionary();

    public abstract string GetTopHeadlineQueryOptions();

    public string GetSearchQuery()
    {
        if (ParameterNames.Count == 0)
        {
            SetParameterDictionary();
        }

        string queryString = "?" + string.Join("&", ParameterNames
        .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Value))
        .Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

        Console.WriteLine(queryString);
        return queryString;
    }

}