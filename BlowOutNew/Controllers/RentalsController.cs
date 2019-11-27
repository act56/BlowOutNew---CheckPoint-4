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

            IEnumerable<Instrument> oInstrument = db.Database.SqlQuery<Instrument>
                ("Select * FROM instrument WHERE Type = 'New'; ");            

            return View(oInstrument);
        }

        //db.Instruments.ToList()

        public ActionResult Details(string instName, string instImage)
       {
            ViewBag.Image = instImage;
            ViewBag.Name = instName;
            ViewBag.ContractFull = "Upon completion of the 18 months , the client will owns the instrument. If , however, client returns the instrument at any time " +
                "before 3 months, there will be a $200 re-stocking fee. If the client returns it after 3 months, the client forfeits any equity in the instrument.";

            IEnumerable<Instrument> mInstrument = db.Database.SqlQuery<Instrument>
                ("Select InstrumentID, description, Type, " +
             "Price, ClientID, Image " +
             "FROM Instrument " +
             "WHERE description =" + "'" + instName + "'");

            return View(mInstrument);
        }
    }
}