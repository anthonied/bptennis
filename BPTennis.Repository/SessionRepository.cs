using BPTennis.Data;
using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTennis.Repository
{
    public class SessionRepository
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
        private List<Player> GetActivePlayersForSession(int sessionId)
        {
            using (var model = new bp_tennisEntities())
            {
                var activePlayers = (from sp in model.session_players
                                    where sp.session_id == sessionId
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
                var session = new BPTennis.Data.session_players { player_id = playerId, session_id = sessionId };
                model.session_players.Add(session);
                model.SaveChanges();
            }
        }
        
    }
}
