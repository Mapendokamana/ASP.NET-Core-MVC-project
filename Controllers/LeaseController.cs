using InlandMarinaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace InlandMarinaMVC.Controllers
{

    public class LeaseController : Controller
    {

        // gets current customers slips to display
        [Authorize]
        [HttpGet]
        public ViewResult Index()
        {
            InlandMarinaContext db = new InlandMarinaContext();

            // gets cust id
            var username = User.Identity.Name;
            Customer u = db.Customers.Where(c => c.Username == username).SingleOrDefault();
            var id = u.ID;

            // query to display only current customers slips
            var model = from s in db.Slips
                        join l in db.Leases on s.ID equals l.SlipID
                        join c in db.Customers on l.CustomerID equals c.ID
                        where (l.CustomerID == id)
                        orderby s.ID
                        select s;

            return View(model);
        }

    }
}
