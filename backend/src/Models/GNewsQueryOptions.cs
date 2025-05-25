
public class GNewsQueryOptions() : QueryOptions
{
    protected override void SetParameterDictionary()
    {
        ParameterNames["q"] = SearchKeywords;
        ParameterNames["lang"] = Language;
        ParameterNames["country"] = Country;
        ParameterNames["max"] = NumberOfArticles?.ToString();
        ParameterNames["category"] = Category;
    }

    // We override this in the child, because each API option class would presumably have different search parameter implementations
    public override string GetTopHeadlineQueryOptions()
    {
        string query = GetSearchQuery();
        if (!string.IsNullOrEmpty(SearchKeywords))
        {
            query = query.Replace($"q={Uri.EscapeDataString(SearchKeywords)}", "");
        }

        return query;
    }
}

