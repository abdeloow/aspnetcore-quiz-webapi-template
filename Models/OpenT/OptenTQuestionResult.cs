using Newtonsoft.Json;

namespace QuizApi;

public class OptenTQuestionResult
{
    [JsonProperty("type")]
    public string QuestionType { get; set; }
    [JsonProperty("difficulty")]
    public string Difficulty { get; set; }
    [JsonProperty("category")]
    public string Category { get; set; }
    [JsonProperty("question")]
    public string Question { get; set; }
    [JsonProperty("correct_answer")]
    public string CorrectAnswer { get; set; }
    [JsonProperty("incorrect_answers")]
    public List<string> IncorrectAnswers { get; set; }
}
