using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDBContext _db;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApplicationDBContext db, ILogger<CategoryController> logger)
        {
            _db = db;
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategory = _db.Categories.ToList();
            return View(objCategory);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The display order cannot exactly match the name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully"; 
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Edit(int ? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(Id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
            

            
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The display order cannot exactly match the name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET
        public IActionResult Delete()
        {
            return View("Index");
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int?Id)
        {
            try
            {
                if (Id == null || Id == 0)
                {
                    return NotFound();
                }

                var obj = _db.Categories.Find(Id);
                if (obj == null)
                {
                    return NotFound();
                }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");

            }
            catch(Exception e)
            {
                _logger.LogError(e, "An exception occured in DeletePOST");
                throw;
            }
        }



    }
}
