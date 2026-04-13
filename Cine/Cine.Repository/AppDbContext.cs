using Cine.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cine.Repository;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Movie> Movies => Set<Movie>();
    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<Session> Sessions => Set<Session>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();
            entity.Property(m => m.Title).IsRequired();

            entity.HasData(
                new Movie { Id = 1, Title = "The Matrix", Stars = 4.8 },
                new Movie { Id = 2, Title = "Interstellar", Stars = 4.7 },
                new Movie { Id = 3, Title = "Inception", Stars = 4.6 }
            );
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedOnAdd();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Password).IsRequired().HasMaxLength(200);
            entity.HasIndex(u => u.Username).IsUnique();

            entity.HasData(
                new User { Id = 1, Username = "demo.user", Password = "demo123" }
            );
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).ValueGeneratedOnAdd();
            entity.Property(s => s.Token).IsRequired().HasMaxLength(128);
            entity.Property(s => s.IsActive).IsRequired();
            entity.Property(s => s.ExpirationDate).IsRequired();
            entity.HasIndex(s => s.Token).IsUnique();

            entity.HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new Session
                {
                    Id = 1,
                    Token = "demo-session-token-123",
                    UserId = 1,
                    IsActive = true,
                    ExpirationDate = new DateTime(2035, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        });
    }
}
