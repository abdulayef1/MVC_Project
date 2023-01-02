
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace WebUI.ViewModels;

public class AppUserLoginVM
{
    [Required]
    public string? EmailOrUsername { get; set; }
    [Required,DataType(DataType.Password)]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}
