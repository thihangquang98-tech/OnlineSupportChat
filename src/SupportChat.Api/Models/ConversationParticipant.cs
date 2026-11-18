using System.ComponentModel.DataAnnotations;

namespace SupportChat.Api.Models;

public class ConversationParticipant
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ConversationId { get; set; }

    public Conversation Conversation { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    public ParticipantRole Role { get; set; }

    public DateTime JoinedAt { get; set; }
        = DateTime.UtcNow;
}