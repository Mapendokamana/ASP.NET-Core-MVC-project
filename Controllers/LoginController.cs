using InlandMarinaData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Controllers
{
    public class LoginController : Controller
    {
        InlandMarinaContext db = new InlandMarinaContext();
        //Route: /Account/Login
        public IActionResult Index(string returnUrl = "")
        {
            if (returnUrl != null)
                TempData["Return Url"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer customer)
        {
            Customer cust = CustomerManager.Authenticate(customer.Username, customer.Password);
            if (cust == null)//authentication failed
            {
                return View(); // return to login page
            }
            // authentication passed
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cust.Username),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies"); // cookie authentication
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookies", claimsPrincipal);

            if (String.IsNullOrEmpty(TempData["Return Url"].ToString())) // no url found
            {
                return RedirectToAction("Index", "Home"); // return to main page
            }
            else
            {
                return Redirect(TempData["Return Url"].ToString()); // return to url
            }
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home"); // return to main page
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
