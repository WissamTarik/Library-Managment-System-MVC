using AutoMapper;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Company.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager,IMapper mapper)
        {
            this._userManager = userManager;
            this._mapper = mapper;
        }
        public IActionResult Index(string SearchInput)
        {
            IEnumerable<UserToReturnDto> Users;
            if (string.IsNullOrEmpty(SearchInput)){

                Users = _userManager.Users.Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                });
            }
            else
            {
                Users = _userManager.Users.Where(u=>u.UserName.ToLower().Contains(SearchInput.ToLower())).Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                });
            }

            return View(Users);
        }

        [HttpGet]
          public async Task<IActionResult> Details(string ?id,string viewName="Details")
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                return NotFound();
            }
            
           
                var dto = _mapper.Map<UserToReturnDto>(user);
            
            return View(viewName,dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id,UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if(model.Id!=id) return BadRequest();
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user is not null) {
                    user.UserName=model.UserName;
                    user.Email=model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    var Result = await _userManager.UpdateAsync(user);

                    if (Result.Succeeded)
                    {
                        TempData["Message"] = $"{model.UserName} is edited successfully";
                        return RedirectToAction("Index");
                    }
                   

                      
                }
            
                return NotFound();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id) {

            if (id is null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if(user is not null)
            {
                var Result=await _userManager.DeleteAsync(user);
                if (Result.Succeeded)
                {
                    TempData["Message"] = $"{user.UserName} is deleted successfully";
                }
            }
            return RedirectToAction("Index");
        }
    
    }
}
