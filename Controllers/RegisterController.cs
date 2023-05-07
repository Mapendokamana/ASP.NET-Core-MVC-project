using InlandMarinaData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Controllers
{
    public class RegisterController : Controller
    {
        InlandMarinaContext db = new InlandMarinaContext();

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Welcome");
            }
            else
            {
                return View(customer);
            }
        }

        [HttpGet]
        public IActionResult Welcome(Customer customer)
        {
            return View();
        }
    }
}
