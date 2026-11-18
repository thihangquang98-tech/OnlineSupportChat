# BÁO CÁO TUẦN 1
## Xây dựng hệ thống chat hỗ trợ trực tuyến

**Dự án:** Online Support Chat  
**Thời gian:** 18/11/2026  
**Giai đoạn:** Tuần 1 – Xây dựng nền tảng và quản lý cuộc trò chuyện

---

### 1. Mục tiêu tuần

- Khởi tạo dự án ASP.NET Core Web API bằng C# và .NET 9.
- Xây dựng cấu trúc thư mục và kiến trúc ban đầu.
- Thiết kế cơ sở dữ liệu SQLite bằng Entity Framework Core.
- Xây dựng các API quản lý cuộc trò chuyện.
- Kiểm thử API bằng Swagger.

### 2. Công nghệ sử dụng

| Công nghệ | Mục đích |
|---|---|
| C# | Ngôn ngữ lập trình |
| .NET 9 | Nền tảng phát triển Web API |
| ASP.NET Core Web API | Xây dựng REST API |
| Entity Framework Core | ORM quản lý dữ liệu |
| SQLite | Hệ quản trị cơ sở dữ liệu |
| Swagger | Kiểm thử và tài liệu API |
| Visual Studio Code | Môi trường lập trình |
| PowerShell | Thực thi lệnh và quản lý dự án |

### 3. Các chức năng đã triển khai

1. Tạo cuộc trò chuyện mới.
2. Lấy danh sách các cuộc trò chuyện.
3. Xem thông tin chi tiết một cuộc trò chuyện.
4. Cho phép nhân viên tham gia cuộc trò chuyện.
5. Chuyển trạng thái cuộc trò chuyện từ Waiting sang Active.
6. Đóng cuộc trò chuyện và ghi nhận thời gian đóng.
7. Ngăn nhân viên tham gia cuộc trò chuyện đã đóng.

### 4. Thiết kế dữ liệu

**Bảng Conversations**

- Id
- CustomerName
- Subject
- Status
- CreatedAt
- ClosedAt

**Bảng ConversationParticipants**

- Id
- ConversationId
- DisplayName
- Role
- JoinedAt

Hai bảng có quan hệ một-nhiều: một cuộc trò chuyện có thể có nhiều người tham gia.

### 5. Các API đã xây dựng

| Phương thức | Endpoint | Chức năng |
|---|---|---|
| POST | /api/Conversations | Tạo cuộc trò chuyện |
| GET | /api/Conversations | Lấy danh sách |
| GET | /api/Conversations/{id} | Xem chi tiết |
| POST | /api/Conversations/{id}/join | Nhân viên tham gia |
| POST | /api/Conversations/{id}/close | Đóng cuộc trò chuyện |

### 6. Kết quả kiểm thử

- Tạo cuộc trò chuyện thành công, trả về HTTP 201.
- Lấy danh sách cuộc trò chuyện thành công.
- Xem chi tiết cuộc trò chuyện thành công.
- Nhân viên tham gia thành công, trạng thái chuyển thành Active.
- Đóng cuộc trò chuyện thành công, trạng thái chuyển thành Closed.
- Thử tham gia lại cuộc trò chuyện đã đóng, hệ thống trả về HTTP 409 Conflict.
- Lệnh `dotnet build` hoàn thành thành công, không có lỗi biên dịch.

### 7. Kết quả đạt được

Đã xây dựng được nền tảng REST API cho hệ thống chat hỗ trợ trực tuyến. Dữ liệu cuộc trò chuyện và người tham gia được quản lý bằng SQLite và Entity Framework Core.

Các chức năng quản lý vòng đời cơ bản của cuộc trò chuyện đã được triển khai và kiểm thử bằng Swagger.

### 8. Công việc dự kiến tuần tiếp theo

- Tìm hiểu SignalR và WebSocket.
- Xây dựng SignalR Hub.
- Thiết lập kết nối thời gian thực giữa khách hàng và nhân viên.
- Kiểm thử gửi và nhận tin nhắn trực tiếp.

---

**Kết luận:** Tuần 1 đã hoàn thành các chức năng nền tảng của hệ thống. Dự án sẵn sàng chuyển sang giai đoạn phát triển giao tiếp thời gian thực bằng SignalR sau khi hoàn tất việc lưu trữ và kiểm tra các minh chứng.