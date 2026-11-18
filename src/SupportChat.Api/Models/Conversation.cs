using System.ComponentModel.DataAnnotations;

namespace SupportChat.Api.Models;

public class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Subject { get; set; } = string.Empty;

    public ConversationStatus Status { get; set; }
        = ConversationStatus.Waiting;

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public DateTime? ClosedAt { get; set; }

    public ICollection<ConversationParticipant> Participants { get; set; }
        = new List<ConversationParticipant>();
}