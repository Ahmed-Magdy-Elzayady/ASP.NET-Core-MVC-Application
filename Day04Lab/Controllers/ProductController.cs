using Day04Lab.Data;
using Day04Lab.Models;
using Day04Lab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace Day04Lab.Controllers
{
    public class ProductController : Controller
    {

        Context db = new Context();

        public IActionResult Index()
        {
            var All_Products = db.Products.Select(i => new ProductsIndexVM
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Price = i.Price,
                Category = i.Category.Name
            });
            return View(All_Products);
        }


        public IActionResult MoreDetails(int id)
        {
            var SpecificItemFromDb = db.Products.Include(o => o.Category).FirstOrDefault(o => o.Id == id);
            if (SpecificItemFromDb is null)
                return NotFound();
            var DetailsVm = new ProductsDetailsVM
            {
                Title = SpecificItemFromDb.Title,
                Description = SpecificItemFromDb.Description,
                Price = SpecificItemFromDb.Price,
                Count = SpecificItemFromDb.Count,
                Category = SpecificItemFromDb.Category.Name,
                Date_Of_Production = SpecificItemFromDb.Date_Of_Production,
                Date_Of_Expire = SpecificItemFromDb.Date_Of_Expire
            };
            return View(DetailsVm);
        }
        [HttpGet]
        public IActionResult CreateNewProduct()
        {
            var ListOfProducts = new ProductsCreateVM
            {
                CategoriesList = GetCategories()
            };

            return View(ListOfProducts);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CreateNewProduct(ProductsCreateVM _NewProduct)
        {
            ModelState.Remove("CategoriesList");

            if (!ModelState.IsValid)
            {
                var res = new ProductsCreateVM
                {
                    CategoriesList = GetCategories()
                };

                return View(res);
            }

            var NewProductToDb = new Products
            {
                Title = _NewProduct.Title,
                Description = _NewProduct.Description,
                Price = _NewProduct.Price,
                Count = _NewProduct.Count,
                CategoriesId = _NewProduct.CategoriesId,
                Date_Of_Production = _NewProduct.Date_Of_Production,
                Date_Of_Expire = _NewProduct.Date_Of_Expire,

            };

            db.Products.Add(NewProductToDb);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult IsTitleAvailable(string _title)
        {
            var IsTitleFound = db.Products.Any(i => i.Title==_title);
            if (IsTitleFound)
                return Json($"Title {_title} is already exist");
            return Json(true);
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {

            var DataToUpdate = db.Products.FirstOrDefault(o => o.Id == id);
            var date = new ProductsCreateVM
            {

                Title = DataToUpdate!.Title,
                Description = DataToUpdate.Description,
                Price = DataToUpdate.Price,
                Count = DataToUpdate.Count,
                Date_Of_Production = DataToUpdate.Date_Of_Production,
                Date_Of_Expire = DataToUpdate.Date_Of_Expire,
                CategoriesList=GetCategories()
            };

            return View(date);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(ProductsCreateVM _UpdatedProduct)
        {
            ModelState.Remove("CategoriesList");
            if (!ModelState.IsValid)
            {
                var ListOfCategories = new ProductsCreateVM
                {
                    CategoriesList = GetCategories()
                };
                return View(ListOfCategories);
            }

            var DataAfterUpdate = db.Products.FirstOrDefault(i => i.Id == _UpdatedProduct.Id);

            DataAfterUpdate!.Title = _UpdatedProduct.Title;
            DataAfterUpdate.Description = _UpdatedProduct.Description;
            DataAfterUpdate.Price = _UpdatedProduct.Price;
            DataAfterUpdate.Count = _UpdatedProduct.Count;
            DataAfterUpdate.Date_Of_Production = _UpdatedProduct.Date_Of_Production;
            DataAfterUpdate.Date_Of_Expire = _UpdatedProduct.Date_Of_Expire;
            DataAfterUpdate.CategoriesId = _UpdatedProduct.CategoriesId;
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> GetCategories()
        {
            return db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();
        }

    }
}

