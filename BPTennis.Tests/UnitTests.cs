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
        private Court _ourGrassTestCourt;
        private IPlayer _testPlayerRepository = new TestPlayerRepository();
        [SetUp]
        public void Given()
        {
            _onePlayerPool = new Pool();
            _player = _getChrisThePlayer();   
            _onePlayerPool.Player.Add(_player);
            _ourGrassTestCourt = _getCourt();
        }

        [Test]
        public void CanCreatePlayer()
        {

            var newPlayer = _player;

            newPlayer.Save();

            Assert.That(_player.Name, Is.EqualTo("Christopher"));
        }

        [Test]
        public void CanCreateCourt()
        {
            Assert.That(_ourGrassTestCourt.CourtName, Is.EqualTo("A1"));
        }

        [Test]
        public void CanSendPlayersToAvailablePool()
        {
            Assert.That(_onePlayerPool.Player.First().Name, Is.EqualTo("Christopher"));
        }

        [Test]
        public void CanSendAvailablePoolPlayerToAvailaleCourt()
        {
            var availablePlayer = _onePlayerPool.Player.First();

            var availableCourts = new List<Court>();

            availableCourts.Add(_ourGrassTestCourt);

            availablePlayer.SendToCourt(availableCourts.First());            

            Assert.That(availableCourts.First().Players.First().Name, Is.EqualTo("Christopher"));

        }

        [Test]
        public void CanRemovePlayerFromPool()
        {
            var availablePlayer = _onePlayerPool.Player.First();

            availablePlayer.RemoveFromPool(_onePlayerPool);

            Assert.That(_onePlayerPool.Player.Find(player => player.Id == availablePlayer.Id), Is.Null);
        }

        [Test]
        public void CourtIsFullWhenFourPlayersAreOnCourt()
        {
           _ourGrassTestCourt.Players = new List<Player>{
                new Player(_testPlayerRepository) { Id = 29, Name = "Christopher"}, 
                new Player(_testPlayerRepository) { Id = 13, Name = "Drilene" }, 
                new Player(_testPlayerRepository) { Id = 17, Name = "Phillip" },
                new Player(_testPlayerRepository) { Id = 12, Name = "Johan" }               
           };

            Assert.That(_ourGrassTestCourt.Full, Is.True);
        }

        [Test]
        public void CourtIsNotFullWhenThreePlayersAreOnCourt()
        {
            _ourGrassTestCourt.Players = new List<Player>{
                new Player(_testPlayerRepository) {Id = 29, Name = "Christopher"},
                new Player(_testPlayerRepository) {Id = 13, Name = "Drilene"},
                new Player(_testPlayerRepository) {Id = 17, Name = "Phillip"}
            };

            Assert.That(_ourGrassTestCourt.Full, Is.False);

        }

        [Test]
        public void CantSendPlayerToCourtWhenCourtIsFull()
        {
            var availablePlayer = _onePlayerPool.Player.First();

            _ourGrassTestCourt.Players = new List<Player>{
                new Player(_testPlayerRepository) {Id = 29, Name = "Christopher"},
                new Player(_testPlayerRepository) {Id = 13, Name = "Drilene"},
                new Player(_testPlayerRepository) {Id = 17, Name = "Phillip"},
                new Player(_testPlayerRepository) {Id = 12, Name = "Johan"}
            };

            availablePlayer.SendToCourt(_ourGrassTestCourt);

            Assert.That(_ourGrassTestCourt.Players.Count, Is.EqualTo(4));
        }

        [Test]
        public void PlayerFinishedOnCourtSendBackToPool()
        {
            var courtPlayer = new Player(_testPlayerRepository) { Id = 29, Name = "Christopher" };

            courtPlayer.AddToPool(_onePlayerPool);

            Assert.That(_ourGrassTestCourt.Players.Find(player => player.Id == courtPlayer.Id), Is.Null);
        }

        [Test]
        public void PlayerAttendedTheDay()
        {
            var attendingPlayer = new Player(_testPlayerRepository) { Id = 29, Name = "Christopher" };

            attendingPlayer.SetToAvailableToPlay();

            Assert.That(attendingPlayer.IsAvailableToPlay, Is.True);
        }

        [Test]
        public void PlayerCanGoHome()
        {
            var tiredPlayer = new Player(_testPlayerRepository) { Id = 7, Name = "Anthonie" };
            tiredPlayer.SetToAvailableToPlay();

            tiredPlayer.SetToNotAvailableToPlay();

            Assert.That(tiredPlayer.IsAvailableToPlay, Is.False);
        }

        [Test]
        public void AbsentPlayerCannotBeAddedToPool()
        {
            var absentPlayer = new Player(_testPlayerRepository) { Id = 7, Name = "Anthonie" };

            absentPlayer.SetToNotAvailableToPlay();

            absentPlayer.AddToPool(_onePlayerPool);

            Assert.That(_onePlayerPool.Player.Count, Is.EqualTo(1));                      
        }

        [Test]
        public void PlayerFinishedMustBeRemovedFromCourt()
        {
            var courtPlayer = new Player(_testPlayerRepository) { Id = 13, Name = "Drilene" };

            courtPlayer.SendToCourt(_ourGrassTestCourt);

            _ourGrassTestCourt.FinishGame();
            
            Assert.That(_ourGrassTestCourt.Players.Find(player => player.Id == courtPlayer.Id), Is.Null);
        }

        private Court _getCourt()
        {
            var court = new Court()
            {
                CourtName = "A1"
            };
            return court;
        }

        private Player _getChrisThePlayer()
        {
          
            var player = new Player(_testPlayerRepository)
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
