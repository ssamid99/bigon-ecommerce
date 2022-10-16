using BigOn.Domain.Models.FormData;
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

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(UserModel model)
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

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsNotAllowed)
            {
                return RedirectToAction("Username", "Girish uchu sizin icazeniz yoxdur");
            }
            else if (result.IsLockedOut)
            {
                return RedirectToAction("Username", "5deq sonra yeniden");
            }

            end:
            return View(model);
        }
    }
}
