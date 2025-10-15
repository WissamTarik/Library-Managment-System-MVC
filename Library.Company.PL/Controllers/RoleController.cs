using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Threading.Tasks;

namespace Library.Company.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> Roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                Roles = await _roleManager.Roles.Select(r => new RoleToReturnDto()
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync() ;

            }
            else
            {
                Roles = await _roleManager.Roles.Where(r=>r.Name.ToLower()
                                           .Contains(SearchInput.ToLower())) 
                                            .Select(r => new RoleToReturnDto()
                                            {
                                                 Id = r.Id,
                                                 Name = r.Name
                                             }).ToListAsync();
            }
            return View(Roles);
        }

        [HttpGet]
      public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
      public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if(role is not null)
                {
                    ModelState.AddModelError("", $"Role {model.Name} is already exist");
                }
                else
                {
                    var NewRole = new IdentityRole()
                    {
                        Name = model.Name
                    };
                    var Result = await _roleManager.CreateAsync(NewRole);
                    if (Result.Succeeded)
                    {
                        TempData["Message"] = $"{model.Name} Role is created :)";
                       return RedirectToAction("Index");
                    }
                    
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string ?id, string viewName="Details")
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return NotFound();
            }


            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewName, dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != id) return BadRequest();
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role is not null)
                {
                    role.Name = model.Name;
                  

                    var Result = await _roleManager.UpdateAsync(role);

                    if (Result.Succeeded)
                    {
                        TempData["Message"] = $"{model.Name} is edited successfully";
                        return RedirectToAction("Index");
                    }



                }

                return NotFound();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if(string.IsNullOrEmpty(id)) return BadRequest();

            var role=await _roleManager.FindByIdAsync(id);
            if (role is not null) {
               var Result=await _roleManager.DeleteAsync(role);
                if (Result.Succeeded)
                {
                    TempData["Message"] = $"{role.Name} is deleted successfully";
                }

            }
            else
            {
                TempData["Message"] = $"{role.Name} is not found";
            }
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string ?roleId)
        {
            if (roleId is null) return BadRequest();
            List<UserInRoleDto> usersInRole=new List<UserInRoleDto>();
            var role =await _roleManager.FindByIdAsync(roleId);

            if (role is null) return BadRequest();

            var users = await _userManager.Users.ToListAsync();
            ViewData["roleId"] = roleId;
            foreach (var user in users)
            {
                var userInRole = new UserInRoleDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,

                };
               if ( await _userManager.IsInRoleAsync(user, role.Name))
                {

                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string?roleId, List<UserInRoleDto> model)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return BadRequest();
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    
                    var user = await _userManager.FindByIdAsync(item.Id);
                    if(!await _userManager.IsInRoleAsync(user,role.Name) && item.IsSelected)
                    {
                        await _userManager.AddToRoleAsync(user,role.Name);
                    }
                    else if(await _userManager.IsInRoleAsync(user,role.Name) && !item.IsSelected)
                    {
                        await _userManager.RemoveFromRoleAsync(user,role.Name);
                    }
                }
                return RedirectToAction("Index", "User");
            }
            return View(model);
        }
    }
}
