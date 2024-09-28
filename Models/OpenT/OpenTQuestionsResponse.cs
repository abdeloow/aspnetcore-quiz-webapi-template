using Newtonsoft.Json;

namespace QuizApi;

public class OpenTQuestionsResponse
{
    [JsonProperty("results")]
    public OptenTQuestionResult[] Results { get; set; }
}
