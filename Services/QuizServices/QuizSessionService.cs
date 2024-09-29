

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
        return await _dbContext.QuizSessions.Include(qs => qs.QuizSessionQuestions)
                           .ThenInclude(qsq => qsq.Question).FirstOrDefaultAsync(qs => qs.SessionId == sessionId);
    }

    public async Task<List<QuizSession>> GetUserQuizSessionsAsync(string userId)
    {
        return await _dbContext.QuizSessions.Where(qs => qs.UserId == userId).Include(qs => qs.QuizSessionQuestions)
                        .ThenInclude(qsq => qsq.Question).ToListAsync();
    }

    public async Task<QuizSession> StartQuizAsync(Configurations configurations, string userId)
    {
        var questions = await _questionService.GetAllQuestionsAsync();
        var selectedQuestions = questions
            .Where(q => q.Category == configurations.Field && q.Difficulty == configurations.Difficulty)
            .OrderBy(r => Guid.NewGuid())
            .Take(configurations.QuestionsAmount)
            .ToList();

        var quizSession = new QuizSession
        {
            SessionId = Guid.NewGuid(),
            UserId = userId,
            StartTime = DateTime.UtcNow,
            IsCompleted = false,
            QuizSessionQuestions = selectedQuestions.Select(q => new QuizSessionQuestion
            {
                Question = q
            }).ToList()
        };

        await _dbContext.QuizSessions.AddAsync(quizSession);
        await _dbContext.SaveChangesAsync();

        return quizSession;
    }
}