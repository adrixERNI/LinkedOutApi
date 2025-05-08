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
        public DbSet<TopicAssessment> TopicAssessments { get; set; }
        public DbSet<SkillFeedback> SkillFeedbacks { get; set; }

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

            modelBuilder.Entity<TopicAssessment>()
                .HasOne(ma => ma.Mentor)
                .WithMany()
                .HasForeignKey(ma => ma.MentorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TopicAssessment>()
                .HasOne(ma => ma.Bootcamper)
                .WithMany()
                .HasForeignKey(ma => ma.BootcamperId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Certification>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certifications)
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
            
            modelBuilder.Entity<CV>()
                .HasOne(c => c.User)
                .WithMany(u => u.CVs)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TopicSkill>()
                .HasIndex(ts => new { ts.TopicId, ts.SkillId })
                .IsUnique();

            modelBuilder.Entity<UserSkill>()
                .HasIndex(us => new { us.UserId, us.SkillId })
                .IsUnique();

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
            modelBuilder.Entity<TopicAssessment>(r =>
            {
                r.HasData(
                    new TopicAssessment
                    {
                        Id = 1,
                        MentorId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                        BootcamperId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"),
                        TopicId = 1,
                        OverallRating = 3,
                        Comments = "attentive and interested in the topic",
                        Tags = "Needs Support"

                    }
                );
            });

            modelBuilder.Entity<Admin>(r =>
            {
                r.HasData(
                    new Admin
                    {
                        Id = Guid.Parse("a1b2c3d4-e5f6-7890-ab12-cdef34567890"),
                        Username = "adminuser",
                        Password = "securepassword"
                    }
                );
            });

            modelBuilder.Entity<Certification>(r =>
            {
                r.HasData(
                    new Certification
                    {
                        Id = 1,
                        Name = "Azure Fundamentals",
                        IssuingOrg = "Microsoft",
                        Expiration = new DateOnly(2026, 12, 31),
                        UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"), // match to existing User
                        SkillId = 14,
                        IsDeleted = false
                    }
                );
            });

            modelBuilder.Entity<TopicAssessment>(r =>
            {
                r.HasData(
                    new TopicAssessment
                    {
                        Id = 2,
                        MentorId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), // Must match seeded User with Mentor role
                        BootcamperId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"), // Must match Bootcamper User
                        TopicId = 1, // Must match a seeded Topic
                        OverallRating = 5,
                        Comments = "Strong understanding of core concepts.",
                        Tags = "html,css,basics",
                        CreatedAt = new DateTime(2024,2,2),
                        UpdatedAt = new DateTime(2025,5,8),
                        IsDeleted = false
                    }
                );
            });

            modelBuilder.Entity<SkillFeedback>(r =>
            {
                r.HasData(
                    new SkillFeedback
                    {
                        Id = 1,
                        Rating = 4,
                        TopicAssessmentId = 1, // Ensure this exists in TopicAssessment seed
                        SkillId = 1,           // Ensure this exists in Skill seed
                        UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"), // Existing User
                        IsDeleted = false
                    }
                );
            });

            modelBuilder.Entity<TopicSkill>(r =>
            {
                r.HasData(
                    new TopicSkill
                    {
                        Id = 1,
                        TopicId = 1, // Ensure this matches a seeded Topic
                        SkillId = 1, // Ensure this matches a seeded Skill
                        IsDeleted = false
                    }
                );
            });
            modelBuilder.Entity<UserSkill>(r =>
            {
                r.HasData(
                    new UserSkill
                    {
                        Id = 1,
                        UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4"), // Existing User
                        SkillId = 1,  // Existing Skill
                        Rating = 3,
                        IsDeleted = false
                    }
                );
            });



        }
    }
}
