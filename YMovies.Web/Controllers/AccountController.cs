using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Services.Service;
using YMovies.Web.Models;
using YMovies.Web.Utilities;

namespace YMovies.Web.Controllers
{
    public class AccountController : Controller
    {
        
        public AccountController()        
        {
            _userService = new UserService(IdentityUserService);
        }

        private IService<UserDto> _userService;

        public IIdentityUserService IdentityUserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IIdentityUserService>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO { Email = model.Email, Password = model.Password };
                var claims = await IdentityUserService.AuthenticateAsync(userDto);
                if (claims == null)
                    ModelState.AddModelError("", "Incorrect login or password");
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false,
                    }, claims);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Login", model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = AutoMap.Mapper.Map<RegisterViewModel, UserDTO>(model);                
                userDto.Role = "user";                
                var operationDetails = await IdentityUserService.CreateAsync(userDto);
                
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await IdentityUserService.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    return View("ResetPassword");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await IdentityUserService.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    var resultDetails = await IdentityUserService.ResetPasswordAsync(model.Email, model.Password);
                    if (!resultDetails.Succedeed)
                        ModelState.AddModelError("", resultDetails.Message);
                    return RedirectToAction("Home", "Index");
                }
            }
            return View(model);
        }
                       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }             
    }
}