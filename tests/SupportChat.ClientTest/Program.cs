
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("==================================");
Console.WriteLine("   SUPPORT CHAT - CLIENT TEST");
Console.WriteLine("==================================");

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5063/chatHub")
    .WithAutomaticReconnect()
    .Build();

// Đăng ký sự kiện nhận tin nhắn
connection.On<string, string, DateTime>(
    "ReceiveMessage",
    (user, message, sentAt) =>
    {
        Console.WriteLine();
        Console.WriteLine($"[{sentAt.ToLocalTime():HH:mm:ss}] {user}: {message}");
        Console.Write("Bạn: ");
    }
);

try
{
    Console.WriteLine("Đang kết nối đến SignalR Hub...");

    await connection.StartAsync();

    Console.WriteLine("Đã kết nối thành công!");
    Console.WriteLine($"Connection ID: {connection.ConnectionId}");
    Console.WriteLine();

    Console.Write("Nhập tên người dùng: ");
    var user = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(user))
    {
        user = "Anonymous";
    }

    Console.WriteLine();
    Console.WriteLine("Nhập nội dung tin nhắn.");
    Console.WriteLine("Gõ /exit để thoát.");
    Console.WriteLine();

    while (true)
    {
        Console.Write("Bạn: ");
        var message = Console.ReadLine();

        if (string.Equals(message, "/exit", StringComparison.OrdinalIgnoreCase))
        {
            break;
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            continue;
        }

        await connection.InvokeAsync(
            "SendMessage",
            user,
            message
        );
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Lỗi: {ex.Message}");
}
finally
{
    await connection.DisposeAsync();
    Console.WriteLine();
    Console.WriteLine("Đã ngắt kết nối.");
}
