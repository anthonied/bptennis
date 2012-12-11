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
                    email = newPlayer.Email
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
    }
}
