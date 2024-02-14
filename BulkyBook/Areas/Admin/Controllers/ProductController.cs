using Bulky.DataAccess.Repositories;
using Bulky.Models;
using Bulky.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Catgories");

            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id <= 0)
            {
                ProductVM productVM = new ProductVM()
                {
                    Product = new Product(),
                    CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.Id.ToString() })
                };
                return View(productVM);
            }
            else
            {
                Product product = _unitOfWork.ProductRepository.Get(p => p.Id == id);
                // we can cross check it...
                // whether product is available or not
                ProductVM productVM = new ProductVM()
                {
                    Product = product,
                    CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.Id.ToString() })
                };
                return View(productVM);

            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVm,IFormFile file)
        {
            //ModelState.ClearValidationState("file");
           
            if (ModelState.IsValid)
            {
               

                    if (file != null){
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string productName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootPath,@"\images\product\");

                        if(!string.IsNullOrEmpty(productVm.Product.ImageUrl))
                        {
                            var oldImagePath =
                                Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }


                        string finalPath = wwwRootPath + @"\images\product\" + productName;

                        using (var fileStream = new FileStream(finalPath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        productVm.Product.ImageUrl = @"\images\product\" + productName;
                    }

                    
                if (productVm.Product.Id != 0)
                {
                    // update
                    _unitOfWork.ProductRepository.Update(productVm.Product);
                    TempData["success"] = "Product Updated Successfully";
                }
                else
                {
                    _unitOfWork.ProductRepository.Add(productVm.Product);
                    TempData["success"] = "Product Created Successfully";
                }

                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            else
            {
                productVm.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.Id.ToString() });
                return View(productVm);
            }
        }



        #region API

        public IActionResult GetAll()
        {
            var obj = _unitOfWork.ProductRepository.GetAll(includeProperties: "Catgories");

            return Json(new {data=obj});
        }


        // Delete 



        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) return Json(new {success=false,message="Problem with delete data"});

            Product product = _unitOfWork.ProductRepository.Get(c => c.Id == id);

            if (product == null) return Json(new { success = false, message = "Problem with delete data" });

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            var oldImagePath =
                                Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }


        #endregion API
    }
}
