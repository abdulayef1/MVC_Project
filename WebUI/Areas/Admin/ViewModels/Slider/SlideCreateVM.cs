using System.ComponentModel.DataAnnotations;


namespace WebUI.Areas.Admin.ViewModels.Slider;

public class SlideCreateVM
{
    [Required]
    public IFormFile? Phote { get; set; }
    [Required, MaxLength(100)]
    public string? Title { get; set; }
    [MaxLength(100)]
    public string? Description { get; set; }
    [MaxLength(100)]
    public string? Offer { get; set; }
}
