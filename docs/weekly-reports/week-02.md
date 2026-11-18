
# WEEK 02 – REAL-TIME CHAT WITH SIGNALR

## 1. Mục tiêu tuần 2

Xây dựng chức năng trao đổi tin nhắn thời gian thực (Real-time Chat) bằng SignalR trong ASP.NET Core Web API.

Các mục tiêu:
- Tìm hiểu cơ chế hoạt động của WebSocket và SignalR.
- Xây dựng SignalR Hub để xử lý tin nhắn.
- Đăng ký và cấu hình SignalR trong ứng dụng ASP.NET Core.
- Xây dựng Console Client để kết nối đến SignalR Hub.
- Kiểm tra chức năng gửi và nhận tin nhắn giữa nhiều Client.
- Kiểm tra khả năng kết nối và ngắt kết nối của Client.

## 2. Công nghệ sử dụng

| Công nghệ | Vai trò |
|---|---|
| C# | Ngôn ngữ lập trình |
| ASP.NET Core Web API | Xây dựng máy chủ |
| SignalR | Giao tiếp thời gian thực |
| SignalR Hub | Tiếp nhận và phát tin nhắn |
| .NET Console App | Mô phỏng người dùng chat |
| Microsoft.AspNetCore.SignalR.Client 9.0.11 | Thư viện kết nối SignalR |
| .NET 9 | Nền tảng phát triển |

## 3. Các chức năng đã xây dựng

### 3.1. Xây dựng ChatHub

Tạo file `Hubs/ChatHub.cs`.

Hub cung cấp phương thức:

- `SendMessage(string user, string message)`: tiếp nhận tin nhắn từ Client.
- `ReceiveMessage`: sự kiện được gửi đến Client để hiển thị tin nhắn.

Các bước xử lý:
1. Kiểm tra tên người gửi.
2. Kiểm tra nội dung tin nhắn.
3. Ghi nhận thời điểm gửi.
4. Phát tin nhắn đến tất cả Client đang kết nối thông qua `Clients.All`.

### 3.2. Đăng ký SignalR trong Program.cs

Cấu hình:

- `builder.Services.AddSignalR()` để đăng ký dịch vụ SignalR.
- `app.MapHub<ChatHub>("/chatHub")` để ánh xạ Hub đến endpoint `/chatHub`.

### 3.3. Xây dựng Console Client

Tạo dự án `SupportChat.ClientTest` tại thư mục `tests`.

Client thực hiện:
- Kết nối đến `http://localhost:5063/chatHub`.
- Đăng ký sự kiện `ReceiveMessage`.
- Gọi phương thức `SendMessage`.
- Hiển thị tin nhắn nhận được.
- Ngắt kết nối khi người dùng nhập `/exit`.

Hai người dùng mô phỏng trong quá trình kiểm thử là Alice và Bob.

## 4. Kiểm thử hệ thống

| STT | Nội dung kiểm thử | Kết quả |
|---|---|---|
| 1 | Khởi động ASP.NET Core API | Thành công |
| 2 | SignalR Negotiation | Thành công |
| 3 | Kiểm tra WebSockets trong Negotiation | Được hỗ trợ |
| 4 | Kiểm tra Server-Sent Events trong Negotiation | Được hỗ trợ |
| 5 | Kiểm tra Long Polling trong Negotiation | Được hỗ trợ |
| 6 | Kết nối Console Client Alice | Thành công |
| 7 | Kết nối Console Client Bob | Thành công |
| 8 | Alice gửi tin nhắn, Bob nhận được | Thành công |
| 9 | Bob gửi tin nhắn, Alice nhận được | Thành công |
| 10 | Client nhận lại tin nhắn do chính mình gửi | Thành công |
| 11 | Ngắt kết nối bằng `/exit` | Thành công |
| 12 | Build API | Thành công |
| 13 | Build Console Client | Thành công |

### 4.1. Kết quả Negotiation

Endpoint kiểm tra:

`POST /chatHub/negotiate?negotiateVersion=1`

Kết quả trả về:
- Có `connectionId`.
- `negotiateVersion = 1`.
- Máy chủ quảng bá hỗ trợ WebSockets.
- Máy chủ quảng bá hỗ trợ Server-Sent Events.
- Máy chủ quảng bá hỗ trợ Long Polling.

Lưu ý: Kết quả Negotiation xác nhận các phương thức truyền tải được máy chủ hỗ trợ, không đồng nghĩa cả ba phương thức đã được kiểm thử kết nối thực tế.

### 4.2. Kiểm thử trao đổi tin nhắn giữa hai Client

Sử dụng hai Console Client độc lập:

- Alice: Client thứ nhất.
- Bob: Client thứ hai.

Kết quả kiểm thử:
1. Alice kết nối thành công đến SignalR Hub.
2. Bob kết nối thành công đến SignalR Hub.
3. Khi Alice gửi tin nhắn, Bob nhận được tin nhắn ngay lập tức.
4. Khi Bob gửi tin nhắn, Alice nhận được tin nhắn ngay lập tức.
5. Người gửi cũng nhận lại tin nhắn do chính mình gửi.

Kết quả: Hai Client có thể trao đổi tin nhắn thời gian thực theo cả hai chiều thông qua SignalR.

### 4.3. Kết quả Build

API:

`dotnet build .\src\SupportChat.Api\SupportChat.Api.csproj`

Kết quả: Build succeeded, 0 Error(s).

Client:

`dotnet build .\tests\SupportChat.ClientTest\SupportChat.ClientTest.csproj`

Kết quả: Build succeeded, 0 Error(s).

### 4.4. Ảnh minh chứng

Các ảnh minh chứng được lưu trong thư mục `docs/screenshots/`:

| Tên ảnh | Nội dung |
|---|---|
| `01-signalr-negotiation.png` | Kết quả SignalR Negotiation và danh sách phương thức truyền tải |
| `02-alice-client.png` | Alice kết nối và gửi tin nhắn |
| `03-bob-client.png` | Hai Client Alice và Bob chạy đồng thời, minh họa trao đổi tin nhắn |

## 5. Cấu trúc dự án được bổ sung

```text
OnlineSupportChat/
│
├── src/
│   └── SupportChat.Api/
│       ├── Hubs/
│       │   └── ChatHub.cs
│       └── Program.cs
│
├── tests/
│   └── SupportChat.ClientTest/
│       ├── Program.cs
│       └── SupportChat.ClientTest.csproj
│
└── docs/
    ├── screenshots/
    │   ├── 01-signalr-negotiation.png
    │   ├── 02-alice-client.png
    │   └── 03-bob-client.png
    │
    └── weekly-reports/
        ├── week-01.md
        └── week-02.md
```

## 6. Kết quả đạt được

Sau tuần 2, dự án đã xây dựng được nền tảng giao tiếp thời gian thực bằng SignalR.

Máy chủ có thể tiếp nhận yêu cầu gửi tin nhắn và phát tin nhắn đến tất cả Client đang kết nối.

Console Client có thể:
- Kết nối đến SignalR Hub.
- Đăng ký sự kiện nhận tin nhắn.
- Gửi tin nhắn đến máy chủ.
- Nhận tin nhắn từ các Client khác.
- Ngắt kết nối khi người dùng yêu cầu.

Đặc biệt, hai Client Alice và Bob đã được kiểm thử trao đổi tin nhắn thành công theo cả hai chiều.

Các dự án API và Client đều biên dịch thành công.

## 7. Những nội dung cần phát triển tiếp

Trong tuần 3:
- Tìm hiểu SignalR Groups.
- Tổ chức Client theo từng cuộc hội thoại.
- Không phát tin nhắn của một cuộc hội thoại đến tất cả người dùng.
- Bổ sung thực thể Message để quản lý tin nhắn.
- Phát triển Chat Service để xử lý nghiệp vụ gửi tin nhắn.

## 8. Kết luận

Tuần 2 đã hoàn thành việc xây dựng và kiểm thử nền tảng giao tiếp thời gian thực bằng SignalR.

Hai Client độc lập Alice và Bob đã kết nối thành công đến SignalR Hub và trao đổi tin nhắn theo cả hai chiều.

Đây là cơ sở quan trọng để phát triển chức năng quản lý phòng chat theo từng cuộc hội thoại bằng SignalR Groups trong tuần tiếp theo.
