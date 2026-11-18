
using Microsoft.AspNetCore.SignalR;

namespace SupportChat.Api.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        // Kiểm tra dữ liệu đầu vào
        if (string.IsNullOrWhiteSpace(user))
        {
            throw new HubException("Tên người gửi không được để trống.");
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new HubException("Nội dung tin nhắn không được để trống.");
        }

        // Thời điểm gửi tin nhắn
        var sentAt = DateTime.UtcNow;

        // Gửi tin nhắn đến tất cả Client đang kết nối
        await Clients.All.SendAsync(
            "ReceiveMessage",
            user,
            message,
            sentAt
        );
    }
}
