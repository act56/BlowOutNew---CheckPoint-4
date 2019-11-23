using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlowOutNew.Models;
using BlowOutNew.DAL;


namespace BlowOutNew.Controllers
{
    public class RentalsController : Controller
    {
        private BlowOutNewContext db = new BlowOutNewContext();
        // GET: Rentals
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Rentals()
        {
            return View(db.Instruments.ToList());
        }

        public ActionResult Details(string instrumentName, string instrumentDesc)
        {
            ViewBag.Name = instrumentName;
            ViewBag.Description = instrumentDesc;
            ViewBag.ContractFull = ("Upon completion of the 18 month agreement, the customer owns the rented instrument.  If, however, the instrument is returned" +
                " at any time before three (3) months, there will be a $200 restocking fee.  If the instrument is returned after three (3) months they forfeit any " +
                "equity in the instrument.");

            return View("Details");
        }
    }
}