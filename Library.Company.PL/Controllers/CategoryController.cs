using Library.Company.BLL.Interfaces;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Company.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string ?SearchInput)
        {
            IEnumerable<Category> Categories;
            if (string.IsNullOrEmpty(SearchInput)) {
            
             Categories= await _unitOfWork.CategoryRepository.GetAllAsync();
            }
            else
            {
                Categories = await _unitOfWork.CategoryRepository.GetByNameAsync(SearchInput);
            }
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Create() {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto model) {
            if (ModelState.IsValid)
            {
                var Category = new Category()
                {
                    Name = model.Name,
                };
               await _unitOfWork.CategoryRepository.AddAsync(Category);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = $"{model.Name} is added successfully !!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int ? id,string ViewName="Details")
         {
            if (id is null) return BadRequest("Invalid id");
            ViewBag.Id = id.Value;
            var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(id.Value);
            if (Category == null) return NotFound();
            var model = new CategoryDto()
            {
                Name = Category.Name
            };
            return View(ViewName,model);
         }



        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int? id,CategoryDto model)
        {
            if (id is null) return BadRequest("Invalid id");
            if (ModelState.IsValid)
            {
                var Category = new Category()
                {
                    Name = model.Name
                };
                Category.Id = id.Value;
                 _unitOfWork.CategoryRepository.Update(Category);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = $"{model.Name} is updated successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
         

        public async Task<IActionResult> Delete([FromRoute] int ? id)
        {
            if (id is null) return BadRequest();
            var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(id.Value);
            if(Category == null) return NotFound();
            _unitOfWork.CategoryRepository.Delete(Category);
            int Count = await _unitOfWork.CompleteAsync();
            if (Count > 0)
            {
                TempData["Message"] = $"{Count} category is deleted";
            }
            return RedirectToAction("Index");
        }
    
    
    }
}
