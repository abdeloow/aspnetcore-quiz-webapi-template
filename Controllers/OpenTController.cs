using Microsoft.AspNetCore.Mvc;

namespace QuizApi;
[ApiController]
[Route("api/[controller]")]
public class OpenTController : ControllerBase
{
    private readonly IOpenTService _openTService;
    public OpenTController(IOpenTService openTService)
    {
        _openTService = openTService;
    }

    [HttpGet("Categories")]
    [ProducesResponseType(typeof(OpenTCategoriesResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OpenTCategoriesResult>> GetCategories()
    {
        var categories = await _openTService.GetCategories();
        if (categories.TriviaCategories.Length == 0) return NoContent();
        return Ok(categories);
    }

    [HttpGet("Questions")]
    [ProducesResponseType(typeof(OpenTQuestionsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OpenTQuestionsResponse>> GetQuestions([FromQuery] int? questionsAmount, [FromQuery] int? category,
    [FromQuery] string? difficulty, [FromQuery] string? type)
    {
        var questions = await _openTService.
        GetQuestions(questionsAmount ?? 10, category ?? 18, difficulty ?? "easy", type ?? "multiple");
        if (!questions.Results.Any())
        {
            return NoContent();
        }
        return Ok(questions);
    }
}
