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

            ViewBag.temp = "Papa Smurf";

            IEnumerable<Instrument> oInstrument = db.Database.SqlQuery<Instrument>
                ("Select * FROM instrument");            

            return View(oInstrument);
        }

        public ActionResult Details(string instName)
       {
            IEnumerable<Instrument> dataDetails = db.Database.SqlQuery<Instrument>
               ("Select * FROM instrument");

            return View(dataDetails);
        }
    }
}