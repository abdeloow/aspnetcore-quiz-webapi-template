using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizApi;

[Authorize]
[ApiController]
[Route("/[controller]")]
public class QuizSessionController : ControllerBase
{
    private readonly IQuizSessionService _quizSessionService;
    private readonly IUserService _userService;
    public QuizSessionController(IQuizSessionService quizSessionService, IUserService userService)
    {
        _quizSessionService = quizSessionService;
        _userService = userService;
    }
    [HttpPost("start")]
    public async Task<ActionResult<QuizSessionDto>> StartQuizSession([FromBody] Configurations configurations)
    {
        var userId = _userService.GetCurrentUserId();
        var quizSession = await _quizSessionService.StartQuizAsync(configurations, userId);
        var quizSessionDto = new QuizSessionDto
        {
            SessionId = quizSession.SessionId,
            StartTime = quizSession.StartTime,
            IsCompleted = quizSession.IsCompleted,
            Questions = quizSession.QuizSessionQuestionResponses.Select(qr => new QuizSessionQuestionDto
            {
                QuestionId = qr.QuestionId,
                Text = qr.Question.QuestionText,
                Answers = qr.Question.Answers.Select(a => new AnswerSubmissionDto
                {
                    AnswerId = a.Id,
                    Text = a.Text
                }).ToList()
            }).ToList()
        };
        return Ok(quizSessionDto);
    }
    // GET: api/quizsession/{sessionId}
    [HttpGet("{sessionId:guid}")]
    public async Task<ActionResult<QuizSessionDto>> GetQuizSession(Guid sessionId)
    {
        var quizSession = await _quizSessionService.GetQuizSessionAsync(sessionId);
        if (quizSession == null)
        {
            return NotFound("Quiz Session Not Found");
        }
        var quizSessionDto = new QuizSessionDto
        {
            SessionId = quizSession.SessionId,
            StartTime = quizSession.StartTime,
            EndTime = quizSession.EndTime,
            IsCompleted = quizSession.IsCompleted,
            Questions = quizSession.QuizSessionQuestionResponses.Select(qr => new QuizSessionQuestionDto
            {
                QuestionId = qr.QuestionId,
                Text = qr.Question.QuestionText,
                Answers = qr.Question.Answers.Select(a => new AnswerSubmissionDto
                {
                    AnswerId = a.Id,
                    Text = a.Text
                }).ToList()
            }).ToList()
        };
        return Ok(quizSessionDto);
    }
    // POST: api/quizsession/{sessionId}/submit
    [HttpPost("{sessionId:guid}/submit")]
    public async Task<IActionResult> SubmitAnswer(Guid sessionId, [FromBody] SubmitAnswerDto submitAnswerDto)
    {
        await _quizSessionService.SubmitAnswerAsync(sessionId, submitAnswerDto.QuestionId, submitAnswerDto.SelectedAnswerId);
        return NoContent();
    }

    [HttpPut("{sessionId:guid}/complete")]
    public async Task<IActionResult> CompleteQuizSession(Guid sessionId)
    {
        await _quizSessionService.CompleteQuizSessionAsync(sessionId);
        return NoContent();
    }
    // GET: api/quizsession/user
    [HttpGet("user")]
    [ProducesResponseType(typeof(List<QuizSessionDto>), 200)]
    public async Task<ActionResult<List<QuizSessionDto>>> GetUserQuizSessions()
    {
        var userId = _userService.GetCurrentUserId();
        var quizSessions = await _quizSessionService.GetUserQuizSessionAsync(userId);

        var quizSessionDtos = quizSessions.Select(quizSession => new QuizSessionDto
        {
            SessionId = quizSession.SessionId,
            StartTime = quizSession.StartTime,
            EndTime = quizSession.EndTime,
            IsCompleted = quizSession.IsCompleted,
            Questions = quizSession.QuizSessionQuestionResponses.Select(qr => new QuizSessionQuestionDto
            {
                QuestionId = qr.QuestionId,
                Text = qr.Question.QuestionText,
                Answers = qr.Question.Answers.Select(a => new AnswerSubmissionDto
                {
                    AnswerId = a.Id,
                    Text = a.Text
                }).ToList(),
                SelectedAnswerId = qr.SelectedAnswerId,
                IsCorrect = qr.IsCorrect
            }).ToList()
        }).ToList();

        return Ok(quizSessionDtos);
    }
    // GET: api/quizsession/{sessionId}/score
    [HttpGet("{sessionId:guid}/score")]
    [ProducesResponseType(typeof(ScoreDto), 200)]
    public async Task<ActionResult<ScoreDto>> CalculateScore(Guid sessionId)
    {
        var quizSession = await _quizSessionService.GetQuizSessionAsync(sessionId);

        if (quizSession == null)
        {
            return NotFound("Quiz session not found.");
        }

        var score = await _quizSessionService.CalculateScoreAsync(sessionId);

        var scoreDto = new ScoreDto
        {
            SessionId = sessionId,
            TotalQuestions = quizSession.QuizSessionQuestionResponses.Count,
            CorrectAnswers = quizSession.QuizSessionQuestionResponses.Count(qr => qr.IsCorrect),
            Score = score
        };

        return Ok(scoreDto);
    }
}
