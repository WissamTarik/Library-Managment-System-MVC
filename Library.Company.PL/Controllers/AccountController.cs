using AutoMapper;
using Library.Company.DAL.Models;
using Library.Company.PL.Dtos;
using Library.Company.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IMapper mapper)
        {
           _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid) { 
                var user= await _userManager.FindByNameAsync(model.UserName);
                 if(user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if(user is null)
                    {
                       user=_mapper.Map<AppUser>(model);

                       var Result= await _userManager.CreateAsync(user,model.Password);

                        if (Result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var item in Result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                    else
                    {
                        ModelState.AddModelError("", "Email is already exist");
                    }
                
                }
                else
                {
                    ModelState.AddModelError("", "User name is already exist");

                }
                ModelState.AddModelError("", "Invalid SignUp");
                 
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user is not null)
                {
                    var Flag =await _userManager.CheckPasswordAsync(user, model.Password);
                    if (Flag)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
               ModelState.AddModelError("", "Invalid SignIn");
              return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
           await  _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }


        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var token=await _userManager.GeneratePasswordResetTokenAsync(user);
                    var Link = Url.Action("ResetPassword", "Account", new
                    {
                        email = model.Email,
                        token
                    },Request.Scheme);

                    var Email = new Email() { 
                        To = model.Email,
                        Body = Link, 
                        Subject = "Reset password"
                    };
                    bool Flag = EmailSettings.SendEmail(Email);
                    if (Flag)
                    {
                        return View("CheckYourInbox");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found");
                }
            }
            ModelState.AddModelError("", "Invalid reset password");
            return View(nameof(ForgetPassword));
        }
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }


        [HttpGet]
        public  async Task<IActionResult> ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
           string email= TempData["email"] as string;
           string token=  TempData["token"] as string;
            if (ModelState.IsValid)
            {
                if (email is null || token is null) return BadRequest();
                var user = await _userManager.FindByEmailAsync(email);
                if(user is not null)
                {
                    var Result = await _userManager.ResetPasswordAsync(user, token, model.Password);

                    if (Result.Succeeded)
                    {
                        TempData["Message"] = "Password is changed successfully";
                        return RedirectToAction("SignIn");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid reset password");
            return View(model);
        }
    }
}

