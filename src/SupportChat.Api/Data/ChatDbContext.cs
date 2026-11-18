
using Microsoft.EntityFrameworkCore;
using SupportChat.Api.Models;

namespace SupportChat.Api.Data;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public DbSet<Conversation> Conversations => Set<Conversation>();

    public DbSet<ConversationParticipant> ConversationParticipants
        => Set<ConversationParticipant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Status)
                .HasConversion<string>();

            entity.HasIndex(c => c.CreatedAt);

            entity.HasMany(c => c.Participants)
                .WithOne(p => p.Conversation)
                .HasForeignKey(p => p.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ConversationParticipant>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Role)
                .HasConversion<string>();

            entity.HasIndex(p => p.ConversationId);
        });
    }
}
