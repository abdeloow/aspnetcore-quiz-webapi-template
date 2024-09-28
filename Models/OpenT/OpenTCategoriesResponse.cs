using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace QuizApi;
public class OpenTCategoriesResponse
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; } = null!;
}
