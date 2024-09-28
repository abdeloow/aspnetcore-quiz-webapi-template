using Microsoft.AspNetCore.Mvc;


namespace QuizApi;
[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;
    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    [HttpGet("/all")]
    [ProducesResponseType(typeof(IEnumerable<QuestionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllQuestions()
    {
        var questions = await _questionService.GetAllQuestionsAsync();
        if (questions == null || !questions.Any()) return NotFound();
        var questionsDtos = questions.Select(q => new QuestionDto
        {
            QuestionText = q.QuestionText,
            Type = q.Type,
            Difficulty = q.Difficulty,
            Category = q.Category,
            AnswerDtos = q.Answers.Select(a => new AnswerDto
            {
                Text = a.Text,
                IsCorrect = a.IsCorrect
            }).ToList()
        });
        return Ok(questionsDtos);
    }

    [HttpGet("/{id:int}")]
    [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuestionDto>> GetQuestionById([FromRoute] int id)
    {
        var existingQuestion = await _questionService.GetQuestionByIdAsync(id);
        if (existingQuestion == null) return NotFound();
        var questionDto = new QuestionDto
        {
            QuestionText = existingQuestion.QuestionText,
            Type = existingQuestion.Type,
            Difficulty = existingQuestion.Difficulty,
            Category = existingQuestion.Category,
            AnswerDtos = existingQuestion.Answers.Select(a => new AnswerDto
            {
                Text = a.Text,
                IsCorrect = a.IsCorrect
            }).ToList()
        };
        return Ok(questionDto);
    }

    [HttpPost()]
    [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuestionDto>> CreateQuestion([FromBody] QuestionDto questionDto)
    {
        if (questionDto == null || string.IsNullOrEmpty(questionDto.QuestionText)) return BadRequest();
        var question = new Question
        {
            QuestionText = questionDto.QuestionText,
            Type = questionDto.Type,
            Difficulty = questionDto.Difficulty,
            Category = questionDto.Category
        };
        question.Answers = questionDto.AnswerDtos.Select(ad => new Answer
        {
            QuestionId = question.Id,
            Question = question,
            Text = ad.Text,
            IsCorrect = ad.IsCorrect
        }).ToList();
        var createdQuestion = await _questionService.CreateQuestionAsync(question);
        if (createdQuestion == null) return StatusCode(StatusCodes.Status500InternalServerError);
        return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, questionDto);
    }

    [HttpDelete("/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
    {
        var existingQuestion = await _questionService.GetQuestionByIdAsync(id);
        if (existingQuestion == null) return NotFound();
        await _questionService.DeleteQuestionByIdAsync(id);
        return NoContent();
    }

    [HttpPut("/{id:int}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuestionDto>> UpdateQuestion([FromRoute] int id, [FromBody] QuestionDto questionDto)
    {
        if (questionDto == null || string.IsNullOrEmpty(questionDto.QuestionText)) return BadRequest();
        var existingQuestion = await _questionService.GetQuestionByIdAsync(id);
        if (existingQuestion == null) return NotFound();
        existingQuestion.QuestionText = questionDto.QuestionText;
        existingQuestion.Type = questionDto.Type;
        existingQuestion.Difficulty = questionDto.Difficulty;
        existingQuestion.Category = questionDto.Category;
        existingQuestion.Answers = questionDto.AnswerDtos.Select(ad => new Answer
        {
            Text = ad.Text,
            IsCorrect = ad.IsCorrect,
            QuestionId = id,
            Question = existingQuestion
        }).ToList();
        var result = await _questionService.UpdateQuestionAsync(id, existingQuestion);
        var updatedQuestionDto = new QuestionDto
        {
            QuestionText = result.QuestionText,
            Type = result.Type,
            Difficulty = result.Difficulty,
            Category = result.Category,
            AnswerDtos = result.Answers.Select(a => new AnswerDto
            {
                Text = a.Text,
                IsCorrect = a.IsCorrect
            }).ToList()
        };
        return CreatedAtAction(nameof(GetQuestionById), new { id = result.Id }, updatedQuestionDto);
    }
}
