using Microsoft.EntityFrameworkCore;

namespace QuizApi;

public class QuestionService : IQuestionService
{
    private readonly AppDbContext _dbContext;
    public QuestionService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Question> CreateQuestionAsync(Question question)
    {
        if (question == null) throw new ArgumentNullException(nameof(question));
        await _dbContext.Questions.AddAsync(question);
        await _dbContext.SaveChangesAsync();
        return question;
    }

    public async Task<Question> DeleteQuestionByIdAsync(int questionId)
    {
        var existingQuestion = await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        if (existingQuestion == null) return null;
        var removedQuestion = _dbContext.Questions.Remove(existingQuestion);
        await _dbContext.SaveChangesAsync();
        return existingQuestion;
    }

    public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        var question = await _dbContext.Questions.Include(q => q.Answers).ToListAsync();
        return question;
    }

    public async Task<Question> GetQuestionByIdAsync(int questionId)
    {
        var question = await _dbContext.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == questionId);
        if (question == null) throw new KeyNotFoundException("No Question was found with the given id");
        return question;
    }

    public async Task<Question> UpdateQuestionAsync(int questionId, Question question)
    {
        if (questionId != question.Id) throw new ArgumentException("Id missmatch");
        if (question == null) throw new ArgumentNullException(nameof(question));
        var existingQuestion = await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        if (existingQuestion == null) throw new KeyNotFoundException("Question Not Found");

        existingQuestion.Id = question.Id;
        existingQuestion.Difficulty = question.Difficulty;
        existingQuestion.Category = question.Category;
        existingQuestion.Type = question.Type;

        var numOfEntities = await _dbContext.SaveChangesAsync();
        if (numOfEntities == 0) throw new InvalidOperationException("Nothing has changed");
        return existingQuestion;
    }
}
