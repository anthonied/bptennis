using BPTennis.Data;
using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BPTennis.Repository
{
    public class PlayerRepository
    {
        
        public void CreatePlayer(Player newPlayer)
        {
            using (var model = new bp_tennisEntities())
            {
                BPTennis.Data.player dbPlayer = new player()
                {
                    name = newPlayer.Name,
                    surname = newPlayer.Surname,
                    gender = newPlayer.Gender,
                    telephone = newPlayer.Telephone,
                    email = newPlayer.Email,
                    status = newPlayer.Status
                };

                model.players.Add(dbPlayer);
                model.SaveChanges();
            }
        }

        public List<Player> GetAllPlayers()
        {
            using (var model = new bp_tennisEntities())
            {
                var players = (from p in model.players
                               where p.status == "Active"
                               select new Player
                              {
                                  Id = p.id,
                                  Name = p.name,
                                  Surname = p.surname,
                                  Gender = p.gender,
                                  Telephone = p.telephone,
                                  Email = p.email
                              }).ToList<Player>();
                return players;
                
            }
        }

        public Player GetPlayerById(int id)
        {
            using (var model = new bp_tennisEntities())
            {
                return (from p in model.players
                       where p.id == id
                       select new Player
                       {
                            Id = p.id,
                            Name = p.name,
                            Surname = p.surname,
                            Gender = p.gender,
                            Telephone = p.telephone,
                            Email = p.email
                       }).FirstOrDefault();                
            }
        }
        public void UpdatePlayerDetails(Player player)
        {
            using (var model = new bp_tennisEntities())
            {
                var dbPlayer = (from p in model.players
                                where p.id == player.Id
                                select p).FirstOrDefault();

                dbPlayer.name = player.Name;
                dbPlayer.surname = player.Surname;
                dbPlayer.gender = player.Gender;
                dbPlayer.telephone = player.Telephone;
                dbPlayer.email = player.Email;

                model.SaveChanges();
            }
        }
        public void ChangePlayerStatus(int id)
        {
            using (var model = new bp_tennisEntities())
            {
                var dbPlayer = (from p in model.players
                                where p.id == id
                                select p).FirstOrDefault();

                dbPlayer.status = "Inactive";
                model.SaveChanges();
            }
        }
        public List<Player> GetAllInactivePlayers()
        {
            using (var model = new bp_tennisEntities())
            {
                var inactivePlayers = (from p in model.players
                                      where p.status == "Inactive"
                                      select new Player()
                                      {
                                          Id = p.id,
                                          Name = p.name,
                                          Surname = p.surname,
                                          Gender = p.gender,
                                          Telephone = p.telephone,
                                          Email = p.email
                                      }).ToList<Player>();
                return inactivePlayers;
            }
        }
        public void ChangeReinstatePlayer(int id)
        {
            using (var model = new bp_tennisEntities())
            {
                var dbPlayer = (from p in model.players
                                where p.id == id
                                select p).FirstOrDefault();

                dbPlayer.status = "Active";
                model.SaveChanges();
            }
        }
        public List<Player> GetPlayersNotInSession(int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var playerIdsAllreadyChosen = from sp in model.session_player
                                              where sp.session_id == sessionId
                                              select sp.player_id;

                var players = (from p in model.players
                               where !playerIdsAllreadyChosen.Contains(p.id)
                              select new Player
                              {
                                  Id = p.id,
                                  Name = p.name,
                                  Surname = p.surname
                              }).ToList<Player>();
                return players;
            }
        }
    }
}
