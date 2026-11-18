
using System.ComponentModel.DataAnnotations;

namespace SupportChat.Api.DTOs;

public class JoinConversationRequest
{
    [Required(ErrorMessage = "Tên nhân viên không được để trống.")]
    [MaxLength(100, ErrorMessage = "Tên nhân viên tối đa 100 ký tự.")]
    public string AgentName { get; set; } = string.Empty;
}
