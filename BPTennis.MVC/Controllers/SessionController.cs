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
        CourtRepository courtRepository = new CourtRepository(); 

        public ActionResult Index()
        {
            var playerRepository = new PlayerRepository();
            var domainSession = sessionRepository.GetTodaySession();            
            var session = new SessionModel { Id = domainSession.Id, Date = domainSession.Date };
            session.Pool.Players = domainSession.ActivePlayers;
            
            session.Courts = courtRepository.GetAllCourts();
            if (session.Id != 0)
                session.Courts.ForEach(court => court.Players = sessionRepository.GetPlayersForCourtForSession(session.Id, court.Id));

            return View(session);
        }

        public ActionResult GainPlayerPosition(int sessionId, string playerId)
        {
            var sessionRepository = new SessionRepository();

            sessionRepository.GainPosition(int.Parse(playerId.Replace("|", "")), sessionId);

            return RedirectToAction("Index");
        }

        public ActionResult DropPlayerPosition(int sessionId, string playerId)
        {
            var sessionRepository = new SessionRepository();

            sessionRepository.DropPlayerPosition(int.Parse(playerId.Replace("|", "")), sessionId);

            return RedirectToAction("Index");
        }

        public ActionResult GetPlayerPosition(int sessionId, string playerId)
        {
            var sessionRepository = new SessionRepository();
            var sessionModel = new SessionModel();

            var playerPosition = sessionRepository.GetPlayerPositionByPlayerId(sessionId, int.Parse(playerId.Replace("|", "")));
            var topPlayer = sessionRepository.GetTopPlayer(sessionId);
            var lastPlayer = sessionRepository.GetLastPlayer(sessionId);
            
            sessionModel.TopPlayerOrder = topPlayer;
            sessionModel.LastPlayerOrder = lastPlayer;

            return RedirectToAction("Index");
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

        public ActionResult EndOfGame(int sessionId, int courtId)
        {
            var endOfGameModel = new EndOfGameModel { SessionId = sessionId, CourtId = courtId };
            var sessionRepository = new SessionRepository();
            endOfGameModel.CourtPlayers = sessionRepository.GetPlayersOnSelectedCourt(sessionId, courtId);
            return View(endOfGameModel);
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
            var playerRepository = new PlayerRepository();
            var court = courtRepository.GetCourtById(courtId);
            var session = sessionRepository.GetTodaySession();

            foreach (string playerId in playerIds)
            {
                var player = playerRepository.GetPlayerById(int.Parse(playerId));
                player.Repository = sessionRepository;
                    player.SendToCourt(court,session);                    
            }                       
            sessionRepository.SaveSessionCourt(court, sessionId);

            return RedirectToAction("Index");
        }

        public ActionResult SendPlayerHome(int sessionId, int playerId)
        {
            sessionRepository.RemovePlayerFromActiveListInSession(playerId);
            return RedirectToAction("Index");
        }

        public ActionResult RemovePlayersFromCourt(int courtId, int sessionId)
        {
            var court = courtRepository.GetCourtForSessionById(courtId, sessionId);
            court.Repository = sessionRepository;
            court.FinishGame(sessionId);
            
            return RedirectToAction("Index");
        }

        public ActionResult CancelEntireGame(int courtId, int sessionId, List<Player> courtPlayers)
        {
            var court = courtRepository.GetCourtForSessionById(courtId, sessionId);
            court.Repository = sessionRepository;
            court.CancelGame(sessionId, courtId);

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
