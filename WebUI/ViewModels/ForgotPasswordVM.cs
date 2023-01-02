using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels;

public class ForgotPasswordVM
{
    [Required, MaxLength(256),DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
}
