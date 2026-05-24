using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Day04Lab.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage ="Lenth Of Product name can not be more Than 30 characters")]
        [MinLength(5,ErrorMessage = "Lenth Of Product name can not be less Than 5 characters")]
        [Remote("IsNameAvailable", "Category",AdditionalFields ="Id")] 
        public string Name { get; set; }

    }
}
