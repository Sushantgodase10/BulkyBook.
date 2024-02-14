using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.CategoryRepository.GetAll().ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // Update 

        public IActionResult Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Category category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // Delete 

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Category category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Category category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);

            if (category == null) return NotFound();

            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
