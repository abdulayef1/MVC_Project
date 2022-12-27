using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ShippingItem
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string? Tittle { get; set; }
    public string? Description { get; set; }
    [Required]
    public string? Photo { get; set; }
   
}
