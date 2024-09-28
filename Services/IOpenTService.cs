namespace QuizApi;

public interface IOpenTService
{
    Task<OpenTCategoriesResult> GetCategories();
    Task<OpenTQuestionsResponse> GetQuestions(int questionAmount, int category, string difficulty, string type);
}
