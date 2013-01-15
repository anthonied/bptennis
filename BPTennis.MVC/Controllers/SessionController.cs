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
    public class SessionController : Controller
    {       
        public ActionResult Index()
        {
            var courtRepository = new CourtRepository();            
            var sessionRepository = new SessionRepository();
            var domainSession = sessionRepository.GetTodaySession();

            var session = new SessionModel { Id = domainSession.Id, Date = domainSession.Date };
            session.Pool.Players = domainSession.ActivePlayers;
            session.Courts = courtRepository.GetAllCourts();
            return View(session);
        }

        //
        // GET: /Session/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Session/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Session/Create

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

        public ActionResult AddActive(int sessionId)
        {
            var addActiveModel = new AddActivePlayerModel { SessionId = sessionId };            
            var playerRepository = new PlayerRepository();
            addActiveModel.AvailablePlayers = playerRepository.GetPlayersNotInSession(sessionId);
            return View(addActiveModel);
        }


        public ActionResult AddActivePlayer(int sessionId, int playerId)
        {
            var sessionRepository = new SessionRepository();

            if (sessionId == 0)
            {                
                sessionId = sessionRepository.CreateNewSessionForToday().Id;
            }

            sessionRepository.AddPlayer(sessionId, playerId);

            return RedirectToAction("Index");

        }

        //
        // GET: /Session/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Session/Edit/5

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
        // GET: /Session/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Session/Delete/5

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
