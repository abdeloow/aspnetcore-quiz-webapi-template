

using Microsoft.EntityFrameworkCore;

namespace QuizApi;

public class QuizSessionService : IQuizSessionService
{
    private readonly AppDbContext _dbContext;
    private readonly IQuestionService _questionService;
    public QuizSessionService(AppDbContext dbContext, IQuestionService questionService)
    {
        _dbContext = dbContext;
        _questionService = questionService;
    }

    public async Task<int> CalculateScoreAsync(Guid sessionId)
    {
        var quizSession = await _dbContext.QuizSessions.Include(qs => qs.QuizSessionQuestionResponses)
            .FirstOrDefaultAsync(qs => qs.SessionId == sessionId);
        if (quizSession == null)
        {
            throw new InvalidOperationException("Quiz session not found");
        }
        var correctAnswers = quizSession.QuizSessionQuestionResponses.Count(r => r.IsCorrect);
        return correctAnswers;
    }

    public async Task CompleteQuizSessionAsync(Guid sessionId)
    {
        var quizSession = await _dbContext.QuizSessions.FindAsync(sessionId);
        if (quizSession != null)
        {
            quizSession.EndTime = DateTime.UtcNow;
            quizSession.IsCompleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<QuizSession> GetQuizSessionAsync(Guid sessionId)
    {
        return await _dbContext.QuizSessions.Include(qs => qs.QuizSessionQuestionResponses)
            .ThenInclude(qsr => qsr.Question).FirstOrDefaultAsync(qs => qs.SessionId == sessionId);
    }

    public async Task<List<QuizSession>> GetUserQuizSessionAsync(string userId)
    {
        return await _dbContext.QuizSessions.Where(qs => qs.UserId == userId).Include(qs => qs.QuizSessionQuestionResponses)
                .ThenInclude(qsr => qsr.Question).ToListAsync();
    }

    public async Task<QuizSession> StartQuizAsync(Configurations configurations, string userId)
    {
        var questions = await _questionService.GetAllQuestionsAsync();
        var selectedAnswer = questions.Where(q => q.Category == configurations.Field && q.Difficulty == configurations.Difficulty)
                            .OrderBy(r => Guid.NewGuid()).Take(configurations.QuestionsAmount).ToList();
        var quizSession = new QuizSession
        {
            SessionId = Guid.NewGuid(),
            UserId = userId,
            StartTime = DateTime.UtcNow,
            IsCompleted = false
        };
        await _dbContext.QuizSessions.AddAsync(quizSession);
        await _dbContext.SaveChangesAsync();
        return quizSession;
    }

    public async Task SubmitAnswerAsync(Guid sessionId, int questionId, int selectedAnswerId)
    {
        var quizSession = await _dbContext.QuizSessions.Include(qs => qs.QuizSessionQuestionResponses)
                .FirstOrDefaultAsync(qs => qs.SessionId == sessionId);
        if (quizSession != null)
        {
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == selectedAnswerId);
            if (selectedAnswer != null)
            {
                var response = new QuizSessionQuestionResponse
                {
                    QuizSessionId = quizSession.SessionId,
                    QuestionId = questionId,
                    SelectedAnswerId = selectedAnswerId,
                    IsCorrect = selectedAnswer.IsCorrect
                };
                quizSession.QuizSessionQuestionResponses.Add(response);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}