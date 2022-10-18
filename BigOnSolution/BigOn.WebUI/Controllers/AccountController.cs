using BigOn.Domain.Models.FormData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<BigOnUser> signInManager;
        private readonly UserManager<BigOnUser> userManager;

        public AccountController(SignInManager<BigOnUser> signInManager, UserManager<BigOnUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        [Route("/signin.html")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/signin.html")]
        public async Task<IActionResult> SignIn(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                goto end;
            }
            var user = await userManager.FindByEmailAsync(model.Username);

            if(user == null)
            {
                ModelState.AddModelError("Username", "Istifadeci adiniz yaxud shifreniz yanlishdir!");
                goto end;
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);

            
            
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("Username", "Girish uchu sizin icazeniz yoxdur");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("Username", "5deq sonra yeniden yoxlayin!");
            }

            var redirectUrl = Request.Query["ReturnUrl"];

            if (!string.IsNullOrWhiteSpace(redirectUrl))
            {
                return Redirect(redirectUrl);
            }

                return RedirectToAction("Index", "Home");

            end:
            return View(model);
        }
    }
}
