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
    public class PlayerController : Controller
    {
        //
        // GET: /Player/

        public ActionResult Index()
        {
            var players = new List<PlayerModel>();

            var playerRepository = new PlayerRepository();

            var domainPlayers = playerRepository.GetAllPlayers();

            domainPlayers.ForEach(domainPlayer => players.Add(new PlayerModel { Name = domainPlayer.Name,
                    Surname = domainPlayer.Surname,
                    Gender = (Gender)Enum.Parse(typeof(Gender),domainPlayer.Gender), Telephone = domainPlayer.Telephone, Email = domainPlayer.Email}));

            return View(players);
        }

        //
        // GET: /Player/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Player/Create

        public ActionResult Create()
        {
            var player = new PlayerModel();
            
            return View(player);
        }

        //
        // POST: /Player/Create

        [HttpPost]
        public ActionResult Create(PlayerModel newModelPlayer)
        {
            try
            {
                var playerRepository = new PlayerRepository();
                playerRepository.CreatePlayer(new Player { Name = newModelPlayer.Name, Surname = newModelPlayer.Surname, Gender = newModelPlayer.Gender.ToString(),
                                                            Telephone = newModelPlayer.Telephone, Email = newModelPlayer.Email});
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Player/Edit/5

        public ActionResult Edit(int id)
        {
            var playerRepository = new PlayerRepository();
            var domainPlayer = new playerRepository.GetPlayerById();

            return View();
        }

        //
        // POST: /Player/Edit/5

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
        // GET: /Player/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Player/Delete/5

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
