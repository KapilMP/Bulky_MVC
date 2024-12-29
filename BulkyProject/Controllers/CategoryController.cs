using BulkyProject.Data;
using BulkyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        //asking for implementation of DbContext that is present in program.cs (service container)
        //by creating constructor with parameter that provides
        //its implemenation (Data/ApplicationDbContext.cs)
        public CategoryController(ApplicationDbContext db)
           
        {
            _db = db;//what implementation we get pass to our local variable
            
        }
        public IActionResult Index()
        {
            //_db can access all the DbSet<T> we added
            List<Category> objCategoryList = _db.Categories.ToList();
            //_db.Categories.ToList(): this will run select * from categories and
            //provide it to objCategoryList

            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Check if the name contains only numbers
            if (int.TryParse(obj.Name, out _) )
            {
                // Add an error to the ModelState
                ModelState.AddModelError("", "The Name cannot be a number.");
            }
            if (ModelState.IsValid)//obj is the ModelState should be valid
                                   //according to data annotation provided in category model
            {
                _db.Categories.Add(obj);//insert the value to the db using .net core
                                        //without needing of insert sql command or statement
                _db.SaveChanges();//execute the statement
                TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // Check if the name contains only numbers
            if (int.TryParse(obj.Name, out _))
            {
                // Add an error to the ModelState
                ModelState.AddModelError("", "The Name cannot be a number.");
            }

            if (ModelState.IsValid)//obj is the ModelState should be valid
                                   //according to data annotation provided in category model
            {
                _db.Categories.Update(obj);//update the value to the db using .net core
                                        //without needing of insert sql command or statement
                _db.SaveChanges();//execute the statement
                TempData["success"] = "Category edited sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
           Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted sucessfully";
            return RedirectToAction("Index");

            
        }
    }
}
