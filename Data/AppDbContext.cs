using LinkedOutApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkedOutApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TopicSkill> TopicSkills { get; set; }
        public DbSet<MentorAssessment> MentorAssessments { get; set; }
        public DbSet<MentorSkillFeedback> MentorSkillFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Batch)
                .WithMany()
                .HasForeignKey(t => t.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>(r => 
            {
                r.HasData(
                    new Role { Id = 1, Name = "User" },
                    new Role { Id = 2, Name = "Admin" }
                );
            });

            modelBuilder.Entity<Category>(r => 
            {
                r.HasData(
                    new Category { Id = 1, Name = "Technical" },
                    new Category { Id = 2, Name = "Soft" },
                    new Category { Id = 3, Name = "Self" }
                );
            });

            modelBuilder.Entity<Skill>(r =>
            {
                r.HasData(
                    new Skill{ Id = 1, Name = "Understanding of Key Concepts" , CategoryId = 1 },
                    new Skill { Id = 2, Name = "Ability to Apply Concepts in Practice", CategoryId =1 },
                    new Skill { Id = 3, Name = "Completion of Assigned Task", CategoryId = 1 },
                    new Skill { Id = 4, Name = "Code Quality", CategoryId = 1 },
                    new Skill { Id = 5, Name = "Use of Best Practices", CategoryId = 1 },
                    new Skill { Id = 6, Name = "Debugging and Troubleshooting", CategoryId = 1 },
                    new Skill { Id = 7, Name = "Velocity", CategoryId = 1 },
                    new Skill { Id = 8, Name = "Participation in Discussions", CategoryId = 2 },
                    new Skill { Id = 9, Name = "Collaboration with Peers", CategoryId = 2 },
                    new Skill { Id = 10, Name = "Ability to Seek Help or Clarify Doubts", CategoryId = 2 },
                    new Skill { Id = 11, Name = "Engagement During the Session", CategoryId = 2 },
                    new Skill { Id = 12, Name = "React", CategoryId = 3 },
                    new Skill { Id = 13, Name = "JavaScript", CategoryId = 3 },
                    new Skill { Id = 14, Name = "C#", CategoryId = 3 },
                    new Skill { Id = 15, Name = "API", CategoryId = 3 },
                    new Skill { Id = 16, Name = "App Security", CategoryId = 3 },
                    new Skill { Id = 17, Name = "Database", CategoryId = 3 },
                    new Skill { Id = 18, Name = "SQL", CategoryId = 3 },
                    new Skill { Id = 19, Name = "AI", CategoryId = 3 },
                    new Skill { Id = 20, Name = "Python", CategoryId = 3 },
                    new Skill { Id = 21, Name = "AWS", CategoryId = 3 },
                    new Skill { Id = 22, Name = "Azure", CategoryId = 3 },
                    new Skill { Id = 23, Name = "Manual Testing", CategoryId = 3 },
                    new Skill { Id = 24, Name = "Automated Testing", CategoryId = 3 },
                    new Skill { Id = 25, Name = "Coding Design Patterns", CategoryId = 3 }
                );
            });

           
        }
    }
}
