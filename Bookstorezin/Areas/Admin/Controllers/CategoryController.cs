using Bookstore.DataAccess.Data;
using Bookstore.DataAccess.Repository.IRepository;
using Bookstore.Models;
using Bookstore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstorezin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
       
        public CategoryController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromunitOfWork = _unitOfWork.Category.Get(u=>u.Id == id);
            //Category? categoryFromunitOfWork1 = _unitOfWork.Categories.FirstOrDefault(x=>x.Id == id);
            //Category? categoryFromunitOfWork2 = _unitOfWork.Categories.Where(x=>x.Id == id).FirstOrDefault();

            if (categoryFromunitOfWork == null)
            {
                return NotFound();
            }
            return View(categoryFromunitOfWork);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromunitOfWork = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromunitOfWork1 = _unitOfWork.Categories.FirstOrDefault(x=>x.Id == id);
            //Category? categoryFromunitOfWork2 = _unitOfWork.Categories.Where(x=>x.Id == id).FirstOrDefault();

            if (categoryFromunitOfWork == null)
            {
                return NotFound();
            }
            return View(categoryFromunitOfWork);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id ==id);

            if (obj == null)
            {
                return NotFound();
            }
            

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
            
            

        }


    }
}
