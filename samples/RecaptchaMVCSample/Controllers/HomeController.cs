/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Recaptcha.Web.Mvc;
using RecaptchaMVCSample.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RecaptchaMVCSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserRegistrationModel model)
        {
            var recaptchaVerifier = this.GetRecaptchaVerifier();
            if (await recaptchaVerifier.VerifyIfSolvedAsync() == false)
            {
                ModelState.AddModelError(String.Empty, "Incorrect CAPTCHA answer.");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Welcome");
            }

            return View(model);
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}
