using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.AppCode.Services;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BigOn.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly BigOnDbContext db;
        private readonly CryptoService crypto;
        private readonly EmailService emailService;

        public HomeController(BigOnDbContext db, CryptoService crypto, EmailService emailService)
        {
            this.db = db;
            this.crypto = crypto;
            this.emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactPost model)
        {
            if (ModelState.IsValid)
            {
                db.ContactPosts.Add(model);
                db.SaveChanges();
                //ViewBag.Message = "Muracietiniz qeyde alindi, tezlikle geri donush edeceyik.";

                //ModelState.Clear();
                //return View();

                var response = new {
                    error = false,
                    message = "Muracietiniz qeyde alindi, tezlikle geri donush edeceyik.",

                };
                return Json(response);
            }
            var errorResponse = new
            {
                error = true,
                message = "Muracietiniz qeyde alinmadi, daxil edilenler yanlishdir",
                state = ModelState.GetErrors()
            };
            return Json(errorResponse);
        }

        public IActionResult Faq()
        {
            var data = db.Faqs.Where(f => f.DeletedDate == null).ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(Subscribe model)
        {
            if (!ModelState.IsValid)
            {
                string msg = ModelState.Values.First().Errors[0].ErrorMessage;
                return Json(new
                {
                    error = true,
                    message = msg
                });
            }

            var entity = db.Subscribes.FirstOrDefault(s => s.Email.Equals(model.Email));

            if (entity != null && entity.IsApproved == true)
            {
                return Json(new
                {
                    error = true,
                    message = "Siz artiq abunesiniz"
                });
            }

            if (entity == null)
            {
                db.Subscribes.Add(model);
                db.SaveChanges();
            }
            else if (entity != null) //email gonderilib ama tesdiq olunmayibsa yeniden gonderende bura dushr
            {
                model.Id = entity.Id;
            }

            string token = $"{model.Id}-{model.Email}-{Guid.NewGuid()}";
            token = crypto.Encrypt(token, true);

            string message = $"Abuneliyinizi <a href='{Request.Scheme}://{Request.Host}/approve-subscribe?token={token}'>link</a> vasitesile tesdiq edin";

            //configuration.SendMail("semedzade1999.ss@gmail.com", message, "Subscribe Confrimation");
            await emailService.SendMailAsync(model.Email, "Subscribe Confrimation", message);
            return Json(new
            {
                error = false,
                message = "Emailinize tesdiq metni gonderdik"
            });
            
        }
        [Route("/approve-subscribe")]
        public string SubscribeApprove(string token)
        {
            

            token = crypto.Decrypt(token);

            Match match = Regex.Match(token, @"^(?<id>\d+)-(?<email>[^-]+)-(?<randomKey>.*)$");

            if (!match.Success)
            {
                return "Token uygun deyil";
            }

            int id = Convert.ToInt32(match.Groups["id"].Value);
            string email = match.Groups["email"].Value;
            string randomKey = match.Groups["randomKey"].Value;

            var entity = db.Subscribes.FirstOrDefault(s => s.Id == id);

            if (entity == null)
            {
                return "Tapilmadi";
            }
            if (entity.IsApproved)
            {
                return ViewBag.Write = "Artiq tesdiq edilib";
            }
            entity.IsApproved = true;
            entity.ApproveDate = DateTime.UtcNow.AddHours(4);
            db.SaveChanges();

            return $"Id: {id} | Email: {email} | Randomkey: {randomKey}";
        }
    }   
}

