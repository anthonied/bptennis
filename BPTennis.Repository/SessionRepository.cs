﻿using BPTennis.Data;
using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BPTennis.Repository
{
    public class SessionRepository:ICourt, IPlayer
    {
        public Session GetTodaySession()
        {            
            using (var model = new bp_tennisEntities())
            {
                var session = (from s in model.sessions
                              where s.date == DateTime.Today
                              select new Session
                              {
                                  Id = s.id,
                                  Date = s.date                                
                              }).FirstOrDefault();

                if (session != null)
                    session.ActivePlayers = GetActivePlayersForSession(session.Id);
                else
                {
                    session = new Session { Date = DateTime.Now };
                    session.ActivePlayers = new List<Player>();
                }
                             
                return session;                         
            }
            
        }

        public int GetSessionIdFromDate(DateTime date)
        {
            using (var model = new bp_tennisEntities())
            {
                var session = (from s in model.sessions
                               where s.date == date
                               select s
                             ).FirstOrDefault();
                return session.id;
            }
            
        }

        private List<Player> GetActivePlayersForSession(int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var activePlayers = (from sp in model.session_players
                                    where sp.session_id == sessionId
                                    orderby sp.player_order
                                    select new Player
                                    {
                                        Id = sp.player.id,
                                        Name = sp.player.name,
                                        Surname = sp.player.surname
                                    }).ToList<Player>();
                return activePlayers;
            }
        }
        public Session CreateNewSessionForToday()
        {
            using (var model = new bp_tennisEntities())
            {
                var session = new BPTennis.Data.session { date = DateTime.Now };
                model.sessions.Add(session);
                model.SaveChanges();

                return GetTodaySession(); 
            }
        }
        public void AddPlayer(int sessionId, int playerId)
        {
            using (var model = new bp_tennisEntities())
            {
                if (model.session_players.Count() == 0)
                {
                    var session = new BPTennis.Data.session_players { player_id = playerId, session_id = sessionId, player_order = 1 };
                    model.session_players.Add(session);
                    model.SaveChanges();
                }
                else
                {
                    var lastPlayerOrder = (from sp in model.session_players
                                           orderby sp.player_order descending
                                           select sp).First();

                    var session = new BPTennis.Data.session_players { player_id = playerId, session_id = sessionId, player_order = lastPlayerOrder.player_order + 1 };
                    model.session_players.Add(session);
                    model.SaveChanges();
                }
            }
        }

        public void SaveSessionCourt(Court court, int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                
                court.Players.ForEach(player => model.session_court_player.Add(new BPTennis.Data.session_court_player { court_id = court.Id,
                        player_id = player.Id, 
                        session_id = sessionId,
                in_progress = true}));
                
                model.SaveChanges();
            }
        }

        public List<Player> GetPlayersForCourtForSession(int sessionId, int courtId)
        {
            using(var model = new bp_tennisEntities())
            {
                var players = (from scp in model.session_court_player
                              where scp.session_id == sessionId && scp.court_id == courtId && scp.in_progress
                              select new Player
                              {               
                                  Id = scp.player_id,
                                  Name = scp.player.name,
                                  Surname = scp.player.surname
                              }).ToList<Player>();
                return players;                               
            }
        }

        public List<Player> PlayersAllreadyOnCourtForSession(int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
            	var playersAllreadyOnCourt = (from scp in model.session_court_player
                                              where scp.in_progress && scp.session_id == sessionId
                                              select new Player
                                              {
                                                  Id = scp.player_id,
                                                  Name = scp.player.name,
                                                  Surname = scp.player.surname
                                              }).ToList<Player>();
                return playersAllreadyOnCourt;
            }
            
        }
        public List<Player> GetPlayersOnSelectedCourt(int sessionId, int courtId)
        {
            using (var model = new bp_tennisEntities())
            {
                var playerOnCourt = (from scp in model.session_court_player
                                      where scp.in_progress && scp.session_id == sessionId && scp.court_id == courtId
                                      select new Player
                                      {
                                          Id = scp.player_id,
                                          Name = scp.player.name,
                                          Surname = scp.player.surname
                                      }).ToList<Player>();
                return playerOnCourt;
            }
        }
        public void RemovePlayersFromCourt(int courtId, int sessionId, int playerId)
        {
            using (var model = new bp_tennisEntities())
            {
                var player = (from scp in model.session_court_player
                                      where scp.court_id == courtId && scp.session_id == sessionId && scp.player_id == playerId
                                     select scp).FirstOrDefault();

                player.in_progress = false;                
                model.SaveChanges();
            }
        }
       
        public void RemovePlayersFromCourt(int courtId)
        {
            using (var model = new bp_tennisEntities())
            {
                var players = from scp in model.session_court_player
                              where scp.court_id == courtId
                              select scp;

                foreach (var player in players)
                {
                    player.in_progress = false;
                }

                model.SaveChanges();
            }
        }

        public void InsertPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void RemovePlayerFromActiveListInSession(int playerId)
        {
            using (var model = new bp_tennisEntities())
            {
                var player = (from sp in model.session_players
                             where sp.player_id == playerId
                             select sp).FirstOrDefault();

                model.session_players.Remove(player);
                model.SaveChanges();
            }
        }

        public void AddPlayersToSessionActivePlayers(int sessionId, List<Player> courtPlayers)
        {
            using (var model = new bp_tennisEntities())
            {
                courtPlayers.ForEach(player => model.session_players.Add(new session_players { player_id = player.Id, session_id = sessionId }));
                model.SaveChanges();
            }
            
        }

        public void CancelCurrentGame(int courtId)
        {
            using (var model = new bp_tennisEntities())
            {
                var players = from scp in model.session_court_player
                              where scp.court_id == courtId
                              select scp;

                foreach (var player in players)
                {
                    model.session_court_player.Remove(player);
                }

                model.SaveChanges();
            }
        }

        public List<PlayerGames> RetrievePlayerGamesForSession(int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var players = (from scp in model.session_court_player
                               where scp.session_id == sessionId
                               group scp by scp.player.name into n
                               select new PlayerGames
                               {
                                   Name = n.Key,
                                   NumberOfGames = n.Count()
                               }).ToList<PlayerGames>();

                return players;
            }                               
        }

        public List<Session> RetrieveAllDaysPlayed()
        {
            using (var model = new bp_tennisEntities())
            {
                var days = (from s in model.sessions
                            select new Session
                            {
                                Date = s.date
                            }).ToList<Session>();
                return days;
            }
        }

        public void GainPosition(int playerId, int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var sessionPlayerToChange = (from sp in model.session_players
                                  where sp.player_id == playerId && sp.session_id == sessionId
                                  select sp).FirstOrDefault();                

                var sessionPlayerToSwapWith = (from sp in model.session_players
                                               where sp.player_order < sessionPlayerToChange.player_order && sp.session_id == sessionId
                                               orderby sp.player_order descending
                                               select sp).First();

                int swapOrder = sessionPlayerToSwapWith.player_order;

                sessionPlayerToSwapWith.player_order = sessionPlayerToChange.player_order;
                sessionPlayerToChange.player_order = swapOrder;
                model.SaveChanges();
            }   
        }
        public void DropPlayerPosition(int playerId, int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var sessionPlayerToChange = (from sp in model.session_players
                                             where sp.player_id == playerId && sp.session_id == sessionId
                                             select sp).FirstOrDefault();

                var sessionPlayerToSwapWith = (from sp in model.session_players
                                               where sp.player_order > sessionPlayerToChange.player_order && sp.session_id == sessionId
                                               orderby sp.player_order ascending
                                               select sp).First();

                int swapOrder = sessionPlayerToSwapWith.player_order;

                sessionPlayerToSwapWith.player_order = sessionPlayerToChange.player_order;
                sessionPlayerToChange.player_order = swapOrder;
                model.SaveChanges();
            }
        }
        public int GetPlayerPositionByPlayerId(int sessionId, int playerId)
        {
            using (var model = new bp_tennisEntities())
            {
                var playerPosition = (from sp in model.session_players
                                      where sp.session_id == sessionId && sp.player_id == playerId
                                      select sp.player_order).FirstOrDefault();
                return playerPosition;
            }
        }

        public int GetTopPlayer(int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var topPlayer = (from sp in model.session_players
                                 where sp.session_id == sessionId
                                 select sp.player_order).First();
                return topPlayer;
            }
        }

        public int GetLastPlayer(int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var lastPlayer = (from sp in model.session_players
                                  where sp.session_id == sessionId
                                  orderby sp.player_order descending
                                  select sp.player_order).First();
                return lastPlayer;
            }
        }
    }
}
