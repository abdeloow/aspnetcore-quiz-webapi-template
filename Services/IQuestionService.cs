namespace QuizApi;

public interface IQuestionService
{
    Task<IEnumerable<Question>> GetAllQuestionsAsync();
    Task<Question> GetQuestionByIdAsync(int questionId);
    Task<Question> DeleteQuestionByIdAsync(int questionId);
    Task<Question> UpdateQuestionAsync(int questionId, Question question);
    Task<Question> CreateQuestionAsync(Question question);
}
