# Online Support Chat

Hệ thống chat hỗ trợ trực tuyến được xây dựng bằng ASP.NET Core Web API, C# và .NET 9.

## 1. Giới thiệu

Online Support Chat là hệ thống hỗ trợ trao đổi trực tuyến giữa khách hàng và nhân viên hỗ trợ.

Dự án được phát triển theo từng giai đoạn. Giai đoạn đầu tập trung xây dựng REST API quản lý cuộc trò chuyện, người tham gia và trạng thái cuộc trò chuyện.

## 2. Công nghệ sử dụng

- C#
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- Visual Studio Code

## 3. Các chức năng hiện tại

- Tạo cuộc trò chuyện.
- Lấy danh sách cuộc trò chuyện.
- Xem chi tiết cuộc trò chuyện.
- Cho phép nhân viên tham gia.
- Chuyển trạng thái từ Waiting sang Active.
- Đóng cuộc trò chuyện.
- Ngăn nhân viên tham gia cuộc trò chuyện đã đóng.

## 4. Cấu trúc dự án

```text
OnlineSupportChat/
├── src/
│   └── SupportChat.Api/
│       ├── Controllers/
│       │   └── ConversationsController.cs
│       ├── Data/
│       │   └── ChatDbContext.cs
│       ├── DTOs/
│       ├── Models/
│       ├── Migrations/
│       ├── Properties/
│       ├── appsettings.json
│       └── Program.cs
├── docs/
│   ├── screenshots/
│   └── weekly-reports/
│       └── week-01.md
└── README.md
```

## 5. Yêu cầu môi trường

- Windows 10 hoặc Windows 11.
- .NET SDK 9.0.
- Visual Studio Code.
- PowerShell.

## 6. Cài đặt và chạy dự án

Mở PowerShell tại thư mục dự án:

```powershell
cd D:\OnlineSupportChat
```

Build dự án:

```powershell
dotnet build .\src\SupportChat.Api\SupportChat.Api.csproj
```

Chạy ứng dụng:

```powershell
cd .\src\SupportChat.Api
dotnet run
```

Sau khi ứng dụng khởi động, mở Swagger tại:

http://localhost:5063/swagger

## 7. Danh sách API

| Method | Endpoint | Chức năng |
|---|---|---|
| GET | /api/Conversations | Lấy danh sách |
| POST | /api/Conversations | Tạo cuộc trò chuyện |
| GET | /api/Conversations/{id} | Xem chi tiết |
| POST | /api/Conversations/{id}/join | Nhân viên tham gia |
| POST | /api/Conversations/{id}/close | Đóng cuộc trò chuyện |

## 8. Trạng thái cuộc trò chuyện

- Waiting: Đang chờ nhân viên hỗ trợ.
- Active: Đang được hỗ trợ.
- Closed: Cuộc trò chuyện đã đóng.

## 9. Báo cáo và minh chứng

- Báo cáo tuần 1: `docs/weekly-reports/week-01.md`
- Ảnh kiểm thử API: `docs/screenshots/`

## 10. Lộ trình phát triển

- Tuần 1: Xây dựng REST API và quản lý cuộc trò chuyện.
- Tuần 2: Tích hợp SignalR để giao tiếp thời gian thực.
- Tuần 3: Xây dựng nhóm chat và quản lý tin nhắn.
- Tuần 4: Tích hợp RabbitMQ.
- Tuần 5: Xây dựng Worker xử lý lịch sử tin nhắn.
- Tuần 6: Hoàn thiện vòng đời cuộc trò chuyện.
- Tuần 7: Xây dựng API truy xuất lịch sử tin nhắn.
- Tuần 8: Kiểm thử tích hợp và hoàn thiện hệ thống.

---

**Dự án đang trong quá trình phát triển.**