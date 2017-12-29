using LuggageTracker.Common;
using LugggeTracker.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LuggageTracker.UnitTest
{
    [TestClass]
    public class DALTests
    {

        IDAL DALContext;
        Luggage luggage = null;
        Passenger passenger = null;

        [TestInitialize]
        public void Initialize()
        {
            DALContext = new InMemDAL();

            luggage = new Luggage(Guid.NewGuid().ToString());

            passenger = new Passenger(1, Guid.NewGuid().ToString(), "TestFirstName", "TestLastName");
        }

        [TestMethod]
        public async Task ShouldAddLuggageSuccessfully()
        {
            string tag = Guid.NewGuid().ToString();
            luggage.TagId = tag;
            await DALContext.AddLuggage(luggage);

            Luggage newLuggage = await DALContext.GetLuggage(tag);
            Assert.AreEqual(tag, newLuggage.TagId);
        }

        [TestMethod]
        public async Task ShouldAddPassengerSuccessfully()
        {
            UInt64 Id = 1;
            passenger.PassengerId = Id;
            await DALContext.AddPassenger(passenger);

            Passenger newPassenger = await DALContext.GetPassenger(Id);
            Assert.AreEqual(newPassenger.PassengerId, Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ShouldFailToAddNullLuggage()
        {
            await DALContext.AddLuggage(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ShouldFailToAddNullPassenger()
        {
            await DALContext.AddPassenger(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddPassengerWithBlankPNR()
        {
            passenger.PNR = "";
            await DALContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddPassengerWithNullPNR()
        {
            passenger.PNR = null;
            await DALContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddPassengerWithBlankPassengerFirstName()
        {
            passenger.PassengerFirstName = "";
            await DALContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddPassengerWithNullPassengerFirstName()
        {
            passenger.PassengerFirstName = null;
            await DALContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddPassengerWithBlankPassengerLastName()
        {
            passenger.PassengerLastName = "";
            await DALContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddPassengerWithNullPassengerLastName()
        {
            passenger.PassengerLastName = null;
            await DALContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddPassengerWithPassengerIdBelow1()
        {
            passenger.PassengerId = 0;
            await DALContext.AddPassenger(passenger);
        }



        [TestMethod]
        public async Task ShouldUpdateLuggageSuccessfully()
        {
            string tag = Guid.NewGuid().ToString();
            luggage.TagId = tag;
            await DALContext.AddLuggage(luggage);

            Luggage newLuggage = await DALContext.GetLuggage(tag);
            Assert.AreEqual(tag, newLuggage.TagId);
        }

        [TestMethod]
        public async Task ShouldUpdatePassengerSuccessfully()
        {
            UInt64 Id = 1;
            passenger.PassengerId = Id;
            await DALContext.AddPassenger(passenger);

            Passenger newPassenger = await DALContext.GetPassenger(Id);
            Assert.AreEqual(newPassenger.PassengerId, Id);
        }

    }
}
