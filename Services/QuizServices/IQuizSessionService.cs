namespace QuizApi;

public interface IQuizSessionService
{
    Task<QuizSession> StartQuizAsync(Configurations configurations, string userId);
    Task<QuizSession> GetQuizSessionAsync(Guid sessionId);
    Task<List<QuizSession>> GetUserQuizSessionAsync(string userId);
    Task CompleteQuizSessionAsync(Guid sessionId);
    Task SubmitAnswerAsync(Guid sessionId, int questionId, int selectedAnswerId);
    Task<int> CalculateScoreAsync(Guid sessionId);
}
