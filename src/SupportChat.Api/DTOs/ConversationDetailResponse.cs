
namespace SupportChat.Api.DTOs;

public class ConversationDetailResponse
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public List<ParticipantResponse> Participants { get; set; }
        = new();
}
