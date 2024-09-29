using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizApi;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<QuizSession> QuizSessions { get; set; }
    public DbSet<QuizSessionQuestion> QuizSessionQuestions { get; set; }
    public DbSet<QuizSessionAnswer> QuizSessionAnswers { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Question>().HasMany(q => q.Answers).WithOne(a => a.Question)
                                        .HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<QuizSession>().HasMany(qs => qs.QuizSessionQuestions).WithOne(q => q.QuizSession)
                                            .HasForeignKey(q => q.QuizSessionId);
        modelBuilder.Entity<QuizSessionQuestion>().HasMany(q => q.QuizSessionAnswers).WithOne(a => a.QuizSessionQuestion)
                                                .HasForeignKey(a => a.QuizSessionQuestionId);
    }

}
