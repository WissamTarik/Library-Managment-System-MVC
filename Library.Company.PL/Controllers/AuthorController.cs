using AutoMapper;
using Library.Company.BLL.Interfaces;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;
using Library.Company.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Threading.Tasks;

namespace Library.Company.PL.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Author> Authors;
            if (string.IsNullOrEmpty(SearchInput))
            {
               
             Authors =  await _unitOfWork.AuthorRepository.GetAllAsync();

            }
            else
            {
                Authors = await _unitOfWork.AuthorRepository.GetByNameAsync(SearchInput);
            }
            return View(Authors);
        }

        [HttpGet]
        public IActionResult Create() {
        
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorDto model) {

            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }
                var AuthorObj = _mapper.Map<Author>(model);
                 await _unitOfWork.AuthorRepository.AddAsync(AuthorObj);
                int Count =await _unitOfWork.CompleteAsync();
                if (Count > 0) {

                   return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int ? id,string ViewName="Details")
        {
            if (id is null) return BadRequest("Invalid id");
            var Author =await _unitOfWork.AuthorRepository.GetByIdAsync(id.Value);
            if (Author == null) return NotFound();
            ViewBag.Id = id.Value;
            var model= _mapper.Map<AuthorDto>(Author);
            return View(ViewName,model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int?id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit([FromRoute] int ? id,AuthorDto model)
        {
            if (id is null) return BadRequest("Invalid id");
            if (ModelState.IsValid)
            {
                if(model.Image is not null && model.ImageName is not null)
                {
                    DocumentSettings.Delete(model.ImageName, "images");
                }
                if(model.Image is not null)
                {
                    model.ImageName=DocumentSettings.UploadFile(model.Image,"images");
                }
                var Author = _mapper.Map<Author>(model);
                Author.Id=id.Value;
                   _unitOfWork.AuthorRepository.Update(Author);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = $"{model.Name} is updated successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int ? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int?id,AuthorDto model)
        {
            if (id is null) return BadRequest("Invalid id");
            if (ModelState.IsValid)
            { 
                if(model.ImageName is not null)
                {
                    DocumentSettings.Delete(model.ImageName, "images");
                }
                var Author= _mapper.Map<Author>(model);
                Author.Id = id.Value;

                _unitOfWork.AuthorRepository.Delete(Author);
                int Count = await _unitOfWork.CompleteAsync();
                if(Count>0)
                {
                    TempData["Message"] = $"{model.Name} is deleted successfully !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
