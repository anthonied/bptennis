using BPTennis.Domain;
using BPTennis.MVC.Models;
using BPTennis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BPTennis.MVC.Controllers
{
    public class CourtController : Controller
    {
        //
        // GET: /Court/

        public ActionResult Index()
        {
            var courts = new List<CourtModel>();

            var courtRepository = new CourtRepository();

            var domainCourts = courtRepository.GetAllCourts();

            domainCourts.ForEach(domainCourt => courts.Add(new CourtModel { Id = domainCourt.Id, CourtName = domainCourt.CourtName }));

            return View(courts);
        }

        //
        // GET: /Court/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Court/Create

        public ActionResult Create()
        {
            var court = new CourtModel();            

            return View(court);
        }

        //
        // POST: /Court/Create

        [HttpPost]
        public ActionResult Create(CourtModel newModelCourt)
        {
            try
            {
                var courtRepository = new CourtRepository();
                courtRepository.CreateCourt(new Court { CourtName = newModelCourt.CourtName });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Court/Edit/5

        public ActionResult Edit(int id)
        {
            var courtRepository = new CourtRepository();

            var domainCourt = courtRepository.GetCourtById(id);

            var courtModel = new CourtModel()
            {
                Id = domainCourt.Id,
                CourtName = domainCourt.CourtName
            };

            return View(courtModel);
        }

        //
        // POST: /Court/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Court court)
        {
            try
            {
                var courtRepository = new CourtRepository();

                courtRepository.UpdateCourtDetails(court);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Court/Delete/5

        public ActionResult Delete(int id)
        {
            var courtRepository = new CourtRepository();
            var domainCourt = courtRepository.GetCourtById(id);

            var courtModel = new CourtModel()
            {
                Id = domainCourt.Id,
                CourtName = domainCourt.CourtName
            };

            return View(courtModel);
        }

        //
        // POST: /Court/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Court court)
        {
            try
            {
                var courtRepository = new CourtRepository();
                courtRepository.RemoveCourt(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
