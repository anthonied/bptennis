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

           // var domainPlayers = playerRepository.GetAllPlayers();
            var domainPlayers = new List<Player>();
            domainPlayers.ForEach(domainPlayer => players.Add(new PlayerModel { Id = domainPlayer.Id,
                    Name = domainPlayer.Name,
                    Surname = domainPlayer.Surname,
                    Gender = (Gender)Enum.Parse(typeof(Gender), domainPlayer.Gender),
                    Telephone = domainPlayer.Telephone,
                    Email = domainPlayer.Email }));

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
            //    var playerRepository = new PlayerRepository();
            //    playerRepository.CreatePlayer(new Player { Name = newModelPlayer.Name, Surname = newModelPlayer.Surname, Gender = newModelPlayer.Gender.ToString(),
             //                                               Telephone = newModelPlayer.Telephone, Email = newModelPlayer.Email, Status = "Active"});
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
            var domainPlayer = playerRepository.GetPlayerById(id);

            var playerModel = new PlayerModel()
            {
                Id = domainPlayer.Id,
                Name = domainPlayer.Name,
                Surname = domainPlayer.Surname,
                Gender = (Gender)Enum.Parse(typeof(Gender),domainPlayer.Gender),
                Telephone = domainPlayer.Telephone,
                Email = domainPlayer.Email
            };

            return View(playerModel);
        }

        //
        // POST: /Player/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Player player)
        {
            try
            {
                var playerRepository = new PlayerRepository();

                playerRepository.UpdatePlayerDetails(player);

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
            var playerRepository = new PlayerRepository();
            var domainPlayer = playerRepository.GetPlayerById(id);

            var playerModel = new PlayerModel()
            {
                Id = domainPlayer.Id,
                Name = domainPlayer.Name,
                Surname = domainPlayer.Surname,
                Gender = (Gender)Enum.Parse(typeof(Gender), domainPlayer.Gender),
                Telephone = domainPlayer.Telephone,
                Email = domainPlayer.Email
            };

            return View(playerModel);
        }

        //
        // POST: /Player/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Player player)
        {
            try
            {
                var playerRepository = new PlayerRepository();
                playerRepository.ChangePlayerStatus(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Inactive()
        {
            var inactivePlayers = new List<PlayerModel>();

            var playerRepository = new PlayerRepository();

            var domainPlayers = playerRepository.GetAllInactivePlayers();

            domainPlayers.ForEach(domainPlayer => inactivePlayers.Add(new PlayerModel
                {
                    Id = domainPlayer.Id,
                    Name = domainPlayer.Name,
                    Surname = domainPlayer.Surname,
                    Gender = (Gender)Enum.Parse(typeof(Gender), domainPlayer.Gender),
                    Telephone = domainPlayer.Telephone,
                    Email = domainPlayer.Email
                }));

            return View(inactivePlayers);
        }

        public ActionResult Reinstate(int id)
        {
            var playerRepository = new PlayerRepository();
            var domainPlayer = playerRepository.GetPlayerById(id);

            var playerModel = new PlayerModel()
            {
                Id = domainPlayer.Id,
                Name = domainPlayer.Name,
                Surname = domainPlayer.Surname,
                Gender = (Gender)Enum.Parse(typeof(Gender), domainPlayer.Gender),
                Telephone = domainPlayer.Telephone,
                Email = domainPlayer.Email
            };

            return View(playerModel);
        }

        [HttpPost]
        public ActionResult Reinstate(int id, Player player)
        {
            try
            {
                var playerRepository = new PlayerRepository();
                playerRepository.ChangeReinstatePlayer(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
