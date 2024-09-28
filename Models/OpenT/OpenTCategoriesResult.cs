using Newtonsoft.Json;

namespace QuizApi;

public class OpenTCategoriesResult
{
    [JsonProperty("trivia_categories")]
    public OpenTCategoriesResponse[] TriviaCategories { get; set; }
}
