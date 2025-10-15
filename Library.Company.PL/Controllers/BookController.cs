using AutoMapper;
using Library.Company.BLL.Interfaces;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Company.PL.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string ?SearchInput)
        {
            IEnumerable<Book> Books;
            if (string.IsNullOrEmpty(SearchInput))
            {

             Books = await _unitOfWork.BookRepository.GetAllAsync();
            }
            else
            {
                Books = await _unitOfWork.BookRepository.GetByNameAsync(SearchInput);
            }
            return View(Books);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDto model)
        {
            if (ModelState.IsValid)
            {
                var Book = _mapper.Map<Book>(model);
                Book.AuthorId = model.AuthorId;
                Book.Author = null;

                Book.Category = null;
             await  _unitOfWork.BookRepository.AddAsync(Book);
                int Count =await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = $"{model.Title} is added successfully";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int ? id,string ViewName="Details")
        {
            if (id is null) return BadRequest("Invalid id");
            var Book = await _unitOfWork.BookRepository.GetByIdAsync(id.Value);
            if (Book != null) {
                if (ViewName != "Details")
                {

                    var bookDtos = _mapper.Map<BookDto>(Book);
                    return View(ViewName, bookDtos);
                }
                else
                {

                    return View(ViewName, Book);

                }

            }
            return NotFound(new
            {
                StatusCode=404,
                Message=$"Book with id {id} is not found"
            });

        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
           return  await Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int ?id,BookDto bookDto)
        {
            if (id is null) return BadRequest("Invalid id");
            if (ModelState.IsValid)
            {

                var Book = _mapper.Map<Book>(bookDto);
                Book.AuthorId = bookDto.AuthorId;
                Book.Author = null;
                Book.Category = null;
                Book.Id = id.Value;
                _unitOfWork.BookRepository.Update(Book);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = $"{bookDto.Title} is updated";
                    return RedirectToAction("Index");
                }

            }
            return View(bookDto);
        }
    
       public async Task<IActionResult> Delete([FromRoute] int ? id )
        {
            if (id is null) return BadRequest("Invalid id");

            var Book =await _unitOfWork.BookRepository.GetByIdAsync(id.Value);
            if (Book is not null)
            {
               _unitOfWork.BookRepository.Delete(Book);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = $"{Book.Title} is deleted";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
