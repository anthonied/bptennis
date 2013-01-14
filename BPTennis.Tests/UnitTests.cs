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
            var ourCourt =  new Court()
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
        public void CanSendAvailablePoolPlayerToAvailaleCourt()
        {
            var availablePlayer = _onePlayerPool.Players.First();

            var availableCourts = new List<Court>();            

            availableCourts.Add(new Court()
            {
                CourtName = "A1"
            });

            availablePlayer.SendToCourt(availableCourts.First());            

            Assert.That(availableCourts.First().Players.First().Name, Is.EqualTo("Christopher"));
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
            var availablePlayer = _onePlayerPool.Players.First();

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

            availablePlayer.SendToCourt(ourCourt);

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
            var courtPlayer = new Player() { Id = 13, Name = "Drilene" };
            var ourCourt = new Court
            {
                CourtName = "A1"
            };
            courtPlayer.SendToCourt(ourCourt);

            ourCourt.FinishGame();

            Assert.That(ourCourt.Players.Find(player => player.Id == courtPlayer.Id), Is.Null);
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
