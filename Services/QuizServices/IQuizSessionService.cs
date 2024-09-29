namespace QuizApi;

public interface IQuizSessionService
{
    Task<QuizSession> StartQuizAsync(Configurations configurations, string userId);
    Task<QuizSession> GetQuizSessionAsync(Guid sessionId);
    Task<List<QuizSession>> GetUserQuizSessionsAsync(string userId);
    Task CompleteQuizSessionAsync(Guid sessionId);
}
