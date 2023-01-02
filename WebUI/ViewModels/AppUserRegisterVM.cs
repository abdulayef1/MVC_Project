using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace WebUI.ViewModels
{
    public class AppUserRegisterVM
    {
        [Required,MaxLength(100)]
        public string? FullName { get; set; }
        [Required,MaxLength(50)]
        public string? Username { get; set; }
        [Required,MaxLength(256),DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }


    }
}
