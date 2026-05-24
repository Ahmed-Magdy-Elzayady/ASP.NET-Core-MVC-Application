using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Day04Lab.Validations;
namespace Day04Lab.ViewModels
{
    public class ProductsCreateVM
    {
        public int Id { get; set; }
        [Required]
        [Remote("IsTitleAvailable","Product")]
        [MinLength(10,ErrorMessage ="Min Length Must be More Than 10 characters")]
        public string Title { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Min Length Must be More Than 10 characters")]
        public string Description { get; set; }
        [Required]
        [Range(5,int.MaxValue,ErrorMessage ="Min Value Must Be Greater Than or Equal 5")]
        public int Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Date Of Production")]
        [ProductionDateValidation]
        public DateOnly Date_Of_Production { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Date Of Expire")]
        [ExpireDateValidation]
        public DateOnly Date_Of_Expire { get; set; }
        //[Required]
        [Display(Name="Category")]
        public int CategoriesId { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
    
    }
}
