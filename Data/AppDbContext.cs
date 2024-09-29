using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizApi;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<QuizSession> QuizSessions { get; set; }
    public DbSet<QuizSessionQuestionResponse> QuizSessionResponses { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<QuizSessionQuestionResponse>().HasOne(q => q.QuizSession).WithMany(qs => qs.QuizSessionQuestionResponses)
                    .HasForeignKey(q => q.QuestionId);
        modelBuilder.Entity<QuizSessionQuestionResponse>().HasOne(q => q.Question).WithMany().HasForeignKey(qr => qr.QuestionId);
        modelBuilder.Entity<QuizSessionQuestionResponse>().HasOne(q => q.SelectedAnswer).WithMany().HasForeignKey(qr => qr.SelectedAnswerId);
    }

}
