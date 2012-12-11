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
            return View();
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
            return View();
        }

        //
        // POST: /Court/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        //
        // POST: /Court/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        //
        // POST: /Court/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
