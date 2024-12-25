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
    }
}
