using System.Web;
using Newtonsoft.Json;

namespace QuizApi;

public class OpenTService : IOpenTService
{
    private readonly HttpClient _client;
    public OpenTService(HttpClient client)
    {
        _client = client;
    }
    public async Task<OpenTCategoriesResult> GetCategories()
    {
        var response = await _client.GetAsync("/api_category.php");
        response.EnsureSuccessStatusCode();
        try
        {
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<OpenTCategoriesResult>(content);
            return categories ?? new OpenTCategoriesResult { };
        }
        catch (JsonException ex)
        {
            return new OpenTCategoriesResult { };
        }
    }

    public async Task<OpenTQuestionsResponse> GetQuestions(int questionAmount, int category, string difficulty, string type)
    {
        var query = HandleQuery(questionAmount, category, difficulty, type);
        var response = await _client.GetAsync(query);
        response.EnsureSuccessStatusCode();
        try
        {
            var content = await response.Content.ReadAsStringAsync();
            var questions = JsonConvert.DeserializeObject<OpenTQuestionsResponse>(content);
            return questions ?? new OpenTQuestionsResponse();
        }
        catch (JsonException ex)
        {
            return new OpenTQuestionsResponse();
        }
    }

    private string HandleQuery(int questionAmount, int category, string difficulty, string type)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["amount"] = Math.Max(questionAmount, 10).ToString();
        if (category > 9 && category < 32)
        {
            query["category"] = category.ToString();
        }
        if (!string.IsNullOrEmpty(difficulty))
        {
            query["difficulty"] = difficulty.ToString();
        }
        if (!string.IsNullOrEmpty(type))
        {
            query["type"] = type.ToString();
        }
        return "/api.php?" + query.ToString();
    }

}
