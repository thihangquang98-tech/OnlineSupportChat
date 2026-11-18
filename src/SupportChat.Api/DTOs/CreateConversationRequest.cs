
using System.ComponentModel.DataAnnotations;

namespace SupportChat.Api.DTOs;

public class CreateConversationRequest
{
    [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
    [MaxLength(100, ErrorMessage = "Tên khách hàng tối đa 100 ký tự.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Chủ đề hỗ trợ không được để trống.")]
    [MaxLength(200, ErrorMessage = "Chủ đề tối đa 200 ký tự.")]
    public string Subject { get; set; } = string.Empty;
}
