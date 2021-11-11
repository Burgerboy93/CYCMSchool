using CYCMSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace CYCMSchool.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Duration> Durations { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<EmailSignature> EmailSignatures { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>().ToTable("Bank");
            modelBuilder.Entity<Duration>().ToTable("Duration");
            modelBuilder.Entity<EmailSignature>().ToTable("EmailSignature");
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<Letter>().ToTable("Letter");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Term>().ToTable("Term");
            modelBuilder.Entity<Tutor>().ToTable("Tutor");

            modelBuilder.Entity<Letter>().ToTable(nameof(Letter))
                .HasMany(c => c.Lessons).WithMany(i => i.Letters);
            modelBuilder.Entity<Student>().HasMany(s => s.Letters)
                .WithOne(s => s.Student).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Letter>().Property(s => s.CreatedDate).HasDefaultValueSql("getDate()");
            
        }

        
    }
}
