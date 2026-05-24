using Day04Lab.Data;
using Day04Lab.Models;
using Day04Lab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day04Lab.Controllers
{
    public class CategoryController : Controller
    {
        Context db = new Context();

        public IActionResult Index()
        {
            var All_Categories = db.Categories.Select(i => new CategoryVM
            {
                Id = i.Id,
                Name = i.Name,
            });
            return View(All_Categories);
        }


        [HttpGet]
        public IActionResult CreateNewCategory()
        {
            return View();
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CreateNewCategory(CategoryVM _NewCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var NewCAtegoryToDb = new Categories
            {
                Name = _NewCategory.Name

            };

            db.Categories.Add(NewCAtegoryToDb);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult IsNameAvailable(string Name,int Id )
        {
            var IsNameFound = db.Categories.Any(i => i.Name == Name && i.Id!= Id);
            if (IsNameFound)
                return Json($"Category with name {Name} is already exist");
            return Json(true);
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {

            var DataToUpdate = db.Categories.FirstOrDefault(o => o.Id == id);
            var date = new CategoryVM
            {

                Name = DataToUpdate.Name
            };

            return View(date);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(CategoryVM _UpdatedCategory)
        {

            var DataAfterUpdate = db.Categories.FirstOrDefault(i => i.Id == _UpdatedCategory.Id);
            DataAfterUpdate.Name = _UpdatedCategory.Name;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult DeleteCategory(int id)
        {
            var UnwantedCategory = db.Categories.FirstOrDefault(i => i.Id == id);
            if (UnwantedCategory is null)
            {
                return NotFound();
            }
            db.Categories.Remove(UnwantedCategory);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
