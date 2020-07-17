using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository repository;

        public CategoryController(ICategoryRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View(repository.GetAll());
        }
        
        [HttpGet]
        public IActionResult AddOrUpdate(int? id)
        {
            if (id==null)
            {
                return View(new Category());

            }
            else
            {
                return View(repository.GetById((int) id));
            }
        }
        [HttpPost]
        public IActionResult AddOrUpdate(Category entity)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(entity);
                TempData["message"] = $"{entity.Name} kayıt edildi";
                return RedirectToAction("List");
            }
            else
            {
                return View(entity);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(repository.GetById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int CategoryId)
        {
            repository.DeleteCategory(CategoryId);
            TempData["message"] = $"{CategoryId} numaralı kayıt silindi";
            return RedirectToAction("List");
        }
    }
}