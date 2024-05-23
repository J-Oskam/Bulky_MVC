using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers {
    public class CategoryController : Controller {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db){
            _db = db;
        }

        public IActionResult Index() {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category) {
            //if(category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("Name", "Name and Display Order cannot be the same.");
            //}

            if (ModelState.IsValid) {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id) {
            if(id==null || id==0) {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            if(categoryFromDb == null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category) {
            if (ModelState.IsValid) {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id) {
            if (id == null || id == 0) {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) {
            Category? category = _db.Categories.Find(id);
            if(category == null) {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}