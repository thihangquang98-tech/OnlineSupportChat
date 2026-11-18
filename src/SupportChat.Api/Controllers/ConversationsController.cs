
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportChat.Api.Data;
using SupportChat.Api.DTOs;
using SupportChat.Api.Models;

namespace SupportChat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversationsController : ControllerBase
{
    private readonly ChatDbContext _context;

    public ConversationsController(ChatDbContext context)
    {
        _context = context;
    }

    // GET: api/conversations
    [HttpGet]
    public async Task<ActionResult<List<ConversationResponse>>> GetConversations()
    {
        var conversations = await _context.Conversations
            .AsNoTracking()
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new ConversationResponse
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                Subject = c.Subject,
                Status = c.Status.ToString(),
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();

        return Ok(conversations);
    }

    // GET: api/conversations/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ConversationDetailResponse>> GetConversation(
        Guid id)
    {
        var conversation = await _context.Conversations
            .AsNoTracking()
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conversation == null)
        {
            return NotFound(new
            {
                message = "Không tìm thấy cuộc trò chuyện."
            });
        }

        var response = new ConversationDetailResponse
        {
            Id = conversation.Id,
            CustomerName = conversation.CustomerName,
            Subject = conversation.Subject,
            Status = conversation.Status.ToString(),
            CreatedAt = conversation.CreatedAt,
            ClosedAt = conversation.ClosedAt,

            Participants = conversation.Participants
                .OrderBy(p => p.JoinedAt)
                .Select(p => new ParticipantResponse
                {
                    Id = p.Id,
                    DisplayName = p.DisplayName,
                    Role = p.Role.ToString(),
                    JoinedAt = p.JoinedAt
                })
                .ToList()
        };

        return Ok(response);
    }

    // POST: api/conversations
    [HttpPost]
    public async Task<ActionResult<ConversationResponse>> CreateConversation(
        [FromBody] CreateConversationRequest request)
    {
        var now = DateTime.UtcNow;

        var conversation = new Conversation
        {
            CustomerName = request.CustomerName,
            Subject = request.Subject,
            Status = ConversationStatus.Waiting,
            CreatedAt = now
        };

        var customer = new ConversationParticipant
        {
            ConversationId = conversation.Id,
            DisplayName = request.CustomerName,
            Role = ParticipantRole.Customer,
            JoinedAt = now
        };

        conversation.Participants.Add(customer);

        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();

        var response = new ConversationResponse
        {
            Id = conversation.Id,
            CustomerName = conversation.CustomerName,
            Subject = conversation.Subject,
            Status = conversation.Status.ToString(),
            CreatedAt = conversation.CreatedAt
        };

        return Created($"/api/conversations/{conversation.Id}", response);
    }

    // POST: api/conversations/{id}/join
    [HttpPost("{id:guid}/join")]
    public async Task<IActionResult> JoinConversation(
        Guid id,
        [FromBody] JoinConversationRequest request)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        var conversation = await _context.Conversations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conversation == null)
        {
            return NotFound(new
            {
                message = "Không tìm thấy cuộc trò chuyện."
            });
        }

        if (conversation.Status == ConversationStatus.Closed)
        {
            return Conflict(new
            {
                message = "Không thể tham gia cuộc trò chuyện đã đóng."
            });
        }

        var updatedRows = await _context.Conversations
            .Where(c => c.Id == id && c.Status != ConversationStatus.Closed)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    c => c.Status,
                    ConversationStatus.Active));

        if (updatedRows == 0)
        {
            return Conflict(new
            {
                message = "Cuộc trò chuyện đã đóng hoặc không thể tham gia."
            });
        }

        var agent = new ConversationParticipant
        {
            Id = Guid.NewGuid(),
            ConversationId = id,
            DisplayName = request.AgentName,
            Role = ParticipantRole.Agent,
            JoinedAt = DateTime.UtcNow
        };

        _context.ConversationParticipants.Add(agent);
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        return Ok(new
        {
            message = "Nhân viên đã tham gia cuộc trò chuyện thành công.",
            conversationId = id,
            agentName = agent.DisplayName,
            role = agent.Role.ToString(),
            status = ConversationStatus.Active.ToString()
        });
    }

    // POST: api/conversations/{id}/close
    [HttpPost("{id:guid}/close")]
    public async Task<IActionResult> CloseConversation(Guid id)
    {
        var conversation = await _context.Conversations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conversation == null)
        {
            return NotFound(new
            {
                message = "Không tìm thấy cuộc trò chuyện."
            });
        }

        if (conversation.Status == ConversationStatus.Closed)
        {
            return Conflict(new
            {
                message = "Cuộc trò chuyện đã được đóng trước đó."
            });
        }

        var closedAt = DateTime.UtcNow;

        var updatedRows = await _context.Conversations
            .Where(c => c.Id == id && c.Status != ConversationStatus.Closed)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Status, ConversationStatus.Closed)
                .SetProperty(c => c.ClosedAt, closedAt));

        if (updatedRows == 0)
        {
            return Conflict(new
            {
                message = "Cuộc trò chuyện đã đóng hoặc không thể đóng."
            });
        }

        return Ok(new
        {
            message = "Đã đóng cuộc trò chuyện thành công.",
            conversationId = id,
            status = ConversationStatus.Closed.ToString(),
            closedAt
        });
    }
}
