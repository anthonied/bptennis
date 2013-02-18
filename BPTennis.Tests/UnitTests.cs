using BPTennis.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTennis.Tests
{
    [TestFixture]
    class UnitTests
    {
        private Player _player;
        private Pool _onePlayerPool;
        private IPlayer _testPlayerRepository = new TestPlayerRepository();


        [SetUp]
        public void Given()
        {
            _onePlayerPool = new Pool();
            _player = _getChrisThePlayer();
            _onePlayerPool.Players.Add(_player);

        }

        [Test]
        public void CanCreateCourt()
        {
            var ourCourt = new Court()
            {
                CourtName = "A1"
            };

            Assert.That(ourCourt.CourtName, Is.EqualTo("A1"));
        }

        [Test]
        public void CanSendPlayersToAvailablePool()
        {
            var playerPool = new Pool();

            playerPool.AddPlayer(new Player { Name = "Christopher" });

            Assert.That(playerPool.Players.First().Name, Is.EqualTo("Christopher"));
        }

        [Test]
        public void CanRemovePlayerFromPool()
        {
            var availablePlayer = _onePlayerPool.Players.First();

            availablePlayer.RemoveFromPool(_onePlayerPool);

            Assert.That(_onePlayerPool.Players.Find(player => player.Id == availablePlayer.Id), Is.Null);
        }

        [Test]
        public void CourtIsFullWhenFourPlayersAreOnCourt()
        {
            var ourCourt = new Court()
            {
                CourtName = "A1"
            };

            ourCourt.Players = new List<Player>{
                new Player() { Id = 29, Name = "Christopher"}, 
                new Player() { Id = 13, Name = "Drilene" }, 
                new Player() { Id = 17, Name = "Phillip" },
                new Player() { Id = 12, Name = "Johan" }               
           };

            Assert.That(ourCourt.Full, Is.True);
        }

        [Test]
        public void CourtIsNotFullWhenThreePlayersAreOnCourt()
        {
            var ourCourt = new Court
            {
                CourtName = "A1"
            };

            ourCourt.Players = new List<Player>{
                new Player() {Id = 29, Name = "Christopher"},
                new Player() {Id = 13, Name = "Drilene"},
                new Player() {Id = 17, Name = "Phillip"}
            };

            Assert.That(ourCourt.Full, Is.False);

        }

        [Test]
        public void CanNotSendPlayerToCourtWhenCourtIsFull()
        {
            var session = new Session();

            var availablePlayer = new Player { Id = 1, Name = "Gerhardus" };
            session.ActivePlayers.Add(availablePlayer);


            var ourCourt = new Court
            {
                CourtName = "A1"
            };

            ourCourt.Players = new List<Player>{
                new Player() {Id = 29, Name = "Christopher"},
                new Player() {Id = 13, Name = "Drilene"},
                new Player() {Id = 17, Name = "Phillip"},
                new Player() {Id = 12, Name = "Johan"}
            };

            availablePlayer.SendToCourt(ourCourt, session);

            Assert.That(ourCourt.Players.Count, Is.EqualTo(4));
        }

        [Test]
        public void PlayerFinishedOnCourtIsNotInTheCourtPlayerListAnyMore()
        {
            var courtPlayer = new Player() { Id = 29, Name = "Christopher" };
            var ourCourt = new Court
            {
                CourtName = "A1"
            };

            courtPlayer.AddToPool(_onePlayerPool);

            Assert.That(ourCourt.Players.Find(player => player.Id == courtPlayer.Id), Is.Null);
        }

        [Test]
        public void PlayerAttendedTheDay()
        {
            var attendingPlayer = new Player() { Id = 29, Name = "Christopher" };

            attendingPlayer.SetToAvailableToPlay();

            Assert.That(attendingPlayer.AvailableToPlay, Is.True);
        }

        [Test]
        public void PlayerCanGoHome()
        {
            var tiredPlayer = new Player() { Id = 7, Name = "Anthonie" };
            tiredPlayer.SetToAvailableToPlay();

            tiredPlayer.SetToNotAvailableToPlay();

            Assert.That(tiredPlayer.AvailableToPlay, Is.False);
        }

        [Test]
        public void AbsentPlayerCannotBeAddedToPool()
        {
            var absentPlayer = new Player() { Id = 7, Name = "Anthonie" };

            absentPlayer.SetToNotAvailableToPlay();

            absentPlayer.AddToPool(_onePlayerPool);

            Assert.That(_onePlayerPool.Players.Count, Is.EqualTo(1));
        }

        [Test]
        public void PlayerFinishedMustBeRemovedFromCourt()
        {
            var session = new Session();
            session.ActivePlayers = new List<Player> { 
                new Player { Id = 1, Name = "Christopher" },
                new Player { Id = 2, Name = "Gerbrand" },
                new Player { Id = 3, Name = "Hernus" },
                new Player { Id = 13, Name = "Drilene" }};

            var courtPlayer = new Player() { Id = 13, Name = "Drilene", Repository = _testPlayerRepository };
            var ourCourt = new Court
            {
                CourtName = "A1"
            };
            ourCourt.Repository = new TestCourtRepository();

            courtPlayer.SendToCourt(ourCourt, session);

            ourCourt.FinishGame(session.Id);

            Assert.That(ourCourt.Players.Find(player => player.Id == courtPlayer.Id), Is.Null);
        }

        [Test]
        public void PlayerSendToCourtIsNotAvailableToPlay()
        {
            var session = new Session();
            session.ActivePlayers = new List<Player> { 
                new Player { Id = 1, Name = "Christopher",Repository = _testPlayerRepository },
                new Player { Id = 2, Name = "Gerbrand" ,Repository = _testPlayerRepository},
                new Player { Id = 3, Name = "Hernus" ,Repository = _testPlayerRepository},
                new Player { Id = 4, Name = "Filamon" ,Repository = _testPlayerRepository}};
            var court = new Court { Id = 1 };

            session.ActivePlayers[2].SendToCourt(court, session);

            Assert.That(session.ActivePlayers.Find(player => player.Id == 3), Is.Null);
        }

        [Test]
        public void CanCancelEntireGame()
        {
            var session = new Session();
            session.ActivePlayers = new List<Player> {
                new Player { Id = 1, Name = "Christopher",Repository = _testPlayerRepository }, 
                new Player { Id = 2, Name = "Gerbrand",Repository = _testPlayerRepository },
                new Player { Id = 3, Name = "Hernus",Repository = _testPlayerRepository },
                new Player { Id = 4, Name = "Filamon",Repository = _testPlayerRepository }};

            var court = new Court { Id = 1, CourtName = "A1" };

            int index = 0;
            //foreach (var player in session.ActivePlayers)
            //{
            //    session.ActivePlayers[index].SendToCourt(court, session);
            //    index++;
            //}

          //  court.CancelGame(session.Id, court.Id);
            Assert.That(court.Players, Is.False);

        }

        [Test]
        public void CurrentTopPlayerIsOneWithLowestPlayerOrder()
        {
            var session = new Session();
            session.ActivePlayers = new List<Player> {
                new Player { Id = 1, Name = "Christopher", PlayerOrder = 10 },
                new Player { Id = 2, Name = "Drilene", PlayerOrder = 7 },
                new Player { Id = 3, Name = "Hernus", PlayerOrder = 8 },
                new Player { Id = 4, Name = "Von-Marie", PlayerOrder = 9 },
                new Player { Id = 5, Name = "Morne", PlayerOrder = 11 }};

            Assert.That(session.CurrentTopPlayer, Is.EqualTo(session.ActivePlayers[1]));
        }

        [Test]
        public void NextPlayerIsCurrentTopPlayerWhenOthersAreSendToCourt()
        {
            var session = new Session();
            session.ActivePlayers = new List<Player> {
                new Player { Id = 1, Name = "Christopher", PlayerOrder = 10,Repository = _testPlayerRepository },
                new Player { Id = 2, Name = "Drilene", PlayerOrder = 7,Repository = _testPlayerRepository },
                new Player { Id = 3, Name = "Hernus", PlayerOrder = 8,Repository = _testPlayerRepository },
                new Player { Id = 4, Name = "Von-Marie", PlayerOrder = 9,Repository = _testPlayerRepository },
                new Player { Id = 5, Name = "Morne", PlayerOrder = 12,Repository = _testPlayerRepository },
                new Player { Id = 6, Name = "Hernus", PlayerOrder = 13,Repository = _testPlayerRepository },
                new Player { Id = 7, Name = "Jan", PlayerOrder = 15,Repository = _testPlayerRepository },
                new Player { Id = 8, Name = "Sollie", PlayerOrder = 11,Repository = _testPlayerRepository }};

                var court = new Court { Id = 1, CourtName = "A" };         
                

                session.ActivePlayers[0].SendToCourt(court, session);
                session.ActivePlayers[0].SendToCourt(court, session);
                session.ActivePlayers[0].SendToCourt(court, session);
                session.ActivePlayers[0].SendToCourt(court, session);


            Assert.That(session.CurrentTopPlayer, Is.EqualTo(session.ActivePlayers.Find(x => x.Id == 8)));
        }

        private Player _getChrisThePlayer()
        {

            var player = new Player()
            {
                Id = 29,
                Name = "Christopher",
                Surname = "Smit",
                Gender = "Male"
            };
            return player;
        }
    }
}
