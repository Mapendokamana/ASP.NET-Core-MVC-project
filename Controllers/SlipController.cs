using InlandMarinaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Controllers
{
    public class SlipController : Controller
    {
        InlandMarinaContext db = new InlandMarinaContext();
        public Lease lease { get; set; }

        // GET: SlipController
         public ActionResult Index(int dockID)
         {
            var session = new InlandMarinaSession(HttpContext.Session);

            // creates viewbag for DockID getting all distinct ID's from the Slip table
            ViewBag.DockID = (from s in db.Slips
                              select s.DockID).Distinct();
            
            // query to get all slips that are not leased
            var model = from s in db.Slips
                        orderby s.DockID
                        // filters out leased slips
                        where (!(from lease in db.Leases
                                 select lease.SlipID).Contains(s.ID))
                        // gets dockID's to select from links, still shows all slips even if dockID is not selected
                        where s.DockID == dockID || dockID == 0
                        select s;

            return View(model);
         }


        // shows details of selected slip
        public ActionResult Details(int id)
        {
            var session = new InlandMarinaSession(HttpContext.Session);

            // query to find slip
            var model = new SlipViewModel
            {
                Slip = db.Slips
                    .Where(s => s.ID == id)
                    .FirstOrDefault()
            };

            return View(model);
        }

        // adds new lease to lease table
        [Authorize]
        [HttpPost]
        public RedirectToActionResult Add(SlipViewModel model)
        {
            // gets slip from selected slip
           model.Slip = db.Slips
                        .Where(s => s.ID == model.Slip.ID)
                        .FirstOrDefault();

            var session = new InlandMarinaSession(HttpContext.Session);
            var slip = session.GetMySlips();
            slip.Add(model.Slip);
            session.SetMySlips(slip);

            var cookies = new InlandMarinaCookies(Response.Cookies);
            cookies.SetMySlipsIds(slip);

            // creates new lease
            this.lease = new Lease();

            // gets current user and stores in variable
            var username = User.Identity.Name;

            // finds user id from the current users username
            Customer u = db.Customers.Where(c => c.Username == username).SingleOrDefault();

            // stores current customer id in variable
            var id = u.ID;

            // sets new lease customer id to current cust
            lease.CustomerID = id;
            lease.SlipID = model.Slip.ID;

            // adds and saves lease
            db.Leases.Add(lease);
            db.SaveChanges();

            // redirects to My Slips page
            return RedirectToAction("Index", "Lease");

        }

    }
}
