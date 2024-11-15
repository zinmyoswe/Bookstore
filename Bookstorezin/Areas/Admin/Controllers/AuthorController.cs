using Bookstore.DataAccess.Data;
using Bookstore.DataAccess.Repository.IRepository;
using Bookstore.Models;
using Bookstore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstorezin.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public AuthorController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Author> objAuthorList = _unitOfWork.Author.GetAll().ToList();
            return View(objAuthorList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author obj)
        {
            

            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Author created successfully";
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
            Author? AuthorFromunitOfWork = _unitOfWork.Author.Get(u => u.Id == id);
            //Author? AuthorFromunitOfWork1 = _unitOfWork.Categories.FirstOrDefault(x=>x.Id == id);
            //Author? AuthorFromunitOfWork2 = _unitOfWork.Categories.Where(x=>x.Id == id).FirstOrDefault();

            if (AuthorFromunitOfWork == null)
            {
                return NotFound();
            }
            return View(AuthorFromunitOfWork);
        }

        [HttpPost]
        public IActionResult Edit(Author obj)
        {

            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Author Updated successfully";
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
            Author? AuthorFromunitOfWork = _unitOfWork.Author.Get(u => u.Id == id);
            //Author? AuthorFromunitOfWork1 = _unitOfWork.Categories.FirstOrDefault(x=>x.Id == id);
            //Author? AuthorFromunitOfWork2 = _unitOfWork.Categories.Where(x=>x.Id == id).FirstOrDefault();

            if (AuthorFromunitOfWork == null)
            {
                return NotFound();
            }
            return View(AuthorFromunitOfWork);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Author? obj = _unitOfWork.Author.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }


            _unitOfWork.Author.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Author deleted successfully";
            return RedirectToAction("Index");



        }


    }
}
