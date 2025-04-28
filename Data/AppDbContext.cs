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
                .WithMany(b => b.Topics)
                .HasForeignKey(t => t.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MentorAssessment>()
                .HasOne(ma => ma.Mentor)
                .WithMany()
                .HasForeignKey(ma => ma.MentorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MentorAssessment>()
                .HasOne(ma => ma.Bootcamper)
                .WithMany()
                .HasForeignKey(ma => ma.BootcamperId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Certification>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Certification>()
                .HasOne(c => c.Skill)
                .WithMany()
                .HasForeignKey(c => c.SkillId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<User>(r =>
            {
                r.HasData(
                    new User { 
                        Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), 
                        Name = "Test_Mentor",
                        Email = "sample__bootcampermentor@gmail.com",
                        IsApproved = true,
                        GoogleId = "109846284989882836329",
                        CreatedDate = new DateTime(2024, 1, 1),
                        BatchId = 1,
                        RoleId = 2,
                        Position = "DevOps",
                        //CVId = 1,
                        //ImageId = 1


                    },
                    new User { 
                        Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"), 
                        Name = "Test_Bootcamper",
                        Email = "sample__bootcamper@gmail.com",
                        IsApproved = true,
                        GoogleId = "112906756278986482986",
                        CreatedDate = new DateTime(2024, 1, 1),
                        BatchId = 1,
                        RoleId = 1,
                        //CVId = 1,
                        //ImageId = 1,
                        Bio = "My Bio"

                    }

                );

            });

            modelBuilder.Entity<Topic>(r =>
            {
                r.HasData(
                    new Topic { 
                        Id = 1,
                        Name = "Frontend Development",
                        UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"),
                        BatchId = 1
                    },
                    new Topic { 
                        Id = 2,
                        Name = "Backend Development",
                        UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"),
                        BatchId = 1
                    }
                );
            });

            modelBuilder.Entity<Batch>(r =>
            {
                r.HasData(
                    new Batch
                    {
                        Id = 1,
                        Name = "Backend & Cloud 2025",
                        Status = "In Progress"
                    }
                );
            });

            modelBuilder.Entity<CV>(r =>{
                r.HasData(
                    new CV
                    {
                        Id = 1,
                        Name = "My Resume",
                        File = "Pdf",
                        UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4")
                    }
                );
            });

            modelBuilder.Entity<Image>(r=>
            {
                r.HasData(
                    new Image
                    {
                        Id = 1,
                        Name = "Image1",
                        Path = "Path/heyYou",
                        UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4")
                    }
                );
            });


            modelBuilder.Entity<Role>(r => 
            {
                r.HasData(
                    new Role { Id = 1, Name = "Bootcamper" },
                    new Role { Id = 2, Name = "Mentor" }
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

            modelBuilder.Entity<Project> (r =>
            {
                r.HasData(
                    new Project { Id = 1,
                    Title = "Project 1", 
                    Description = "Description of Project 1", 
                    UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"),
                    TechUsed = "HTML, CSS, JavaScript"
                    }
                );
            });
        }
    }
}
