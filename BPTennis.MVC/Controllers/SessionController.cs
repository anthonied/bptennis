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
        SessionRepository sessionRepository = new SessionRepository();

        public ActionResult Index()
        {
            var courtRepository = new CourtRepository();            
            
            var playerRepository = new PlayerRepository();
            var domainSession = sessionRepository.GetTodaySession();            
            var session = new SessionModel { Id = domainSession.Id, Date = domainSession.Date };
            session.Pool.Players = domainSession.ActivePlayers;
            
            session.Courts = courtRepository.GetAllCourts();
            if (session.Id != 0)
                session.Courts.ForEach(court => court.Players = sessionRepository.GetPlayersForCourtForSession(session.Id, court.Id));

            RemovePlayersFromPoolWhoAreOnCourt(session);

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

        public ActionResult AddPlayerToCourt(int courtId, string courtPlayers, int sessionId)
        {
            var playerIds = courtPlayers.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var courtRepository = new CourtRepository();
            var playerRepository = new PlayerRepository();
            var court = courtRepository.GetCourtById(courtId);
            foreach (string playerId in playerIds)
            {
                var player = playerRepository.GetPlayerById(int.Parse(playerId));
                player.SendToCourt(court);
            }

            var sessionRepository = new SessionRepository();
            sessionRepository.SaveSessionCourt(court, sessionId);

            return RedirectToAction("Index");
        }

        public ActionResult RemovePlayersFromCourt(int courtId, int sessionId)
        {
            var sessionRepository = new SessionRepository();
            var courtRepository = new CourtRepository();
            var court = courtRepository.GetCourtById(courtId);

            var playerIds = sessionRepository.PlayersOnSelectedCourt(sessionId, courtId);

            foreach (var playerId in playerIds)
            {
                //sessionRepository.RemovePlayersFromCourt(courtId, sessionId, playerId);
            }

            sessionRepository.SaveSessionCourt(court, sessionId);


            return RedirectToAction("Index");
        }

        private void RemovePlayersFromPoolWhoAreOnCourt(SessionModel session)
        {
            var playersOnCourtForThisSession = sessionRepository.PlayersAllreadyOnCourtForSession(session.Id);

            
                foreach (var player in playersOnCourtForThisSession)
                {
                    var poolPlayer = session.Pool.Players.Find(p => p.Id == player.Id);
                    if (poolPlayer != null)
                        session.Pool.Players.Remove(poolPlayer);
                }
            
        }
    }
}
