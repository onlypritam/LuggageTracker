namespace LuggageTracker.UnitTest
{
    using LuggageTracker.BL;
    using LuggageTracker.Model;
    using LugggeTracker.DAL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class BLTests
    {
        IDAL DALContext;
        IBL BizContext;
        Luggage luggage = null;
        Passenger passenger = null;

        [TestInitialize]
        public void Initialize()
        {
            DALContext = new InMemDAL();

            BizContext = new LuggageTrackerBizContext(DALContext);

            luggage = new Luggage(Guid.NewGuid().ToString());

            passenger = new Passenger(1, Guid.NewGuid().ToString(), "TestFirstName", "TestLastName");
        }

        [TestMethod]
        public async Task ShouldAddLuggageSuccessfully()
        {
            string tag = Guid.NewGuid().ToString();
            luggage.LuggageId = tag;
            await BizContext.AddLuggage(luggage);

            Luggage newLuggage = await BizContext.GetLuggage(tag);
            Assert.AreEqual(tag, newLuggage.LuggageId);
        }

        [TestMethod]
        public async Task ShouldAddPassengerSuccessfully()
        {
            UInt64 Id = 1;
            passenger.PassengerId = Id;
            await BizContext.AddPassenger(passenger);

            Passenger newPassenger = await BizContext.GetPassenger(Id);
            Assert.AreEqual(newPassenger.PassengerId, Id);
        }

        [TestMethod]
        public async Task ShouldUpdateLuggageSuccessfully()
        {
            string tag = Guid.NewGuid().ToString();
            luggage.LuggageId = tag;
            await BizContext.AddLuggage(luggage);

            Luggage newLuggage = await BizContext.GetLuggage(tag);
            Assert.AreEqual(tag, newLuggage.LuggageId);

            string description = "TestLuggage";
            string weight = "1 KG";
            string name = "TestName";
            string measurement = "1 Meters";
            DateTime lastStatusChange = DateTime.Now;


            newLuggage.Description = description;
            newLuggage.Weight = weight;
            newLuggage.Name = name;
            newLuggage.Measurement = measurement;
            newLuggage.LastStatusChange = lastStatusChange;
            newLuggage.Status = LuggageStatus.CheckedIn;


            await BizContext.UpdateLuggage(newLuggage);
            Luggage updatedLuggage = await DALContext.GetLuggage(tag);
            Assert.AreEqual(tag, updatedLuggage.LuggageId);
            Assert.AreEqual(description, updatedLuggage.Description);
            Assert.AreEqual(weight, updatedLuggage.Weight);
            Assert.AreEqual(name, updatedLuggage.Name);
            Assert.AreEqual(measurement, updatedLuggage.Measurement);
            Assert.AreEqual(lastStatusChange, updatedLuggage.LastStatusChange);
            Assert.AreEqual(LuggageStatus.CheckedIn, updatedLuggage.Status);
        }

        [TestMethod]
        public async Task ShouldUpdatePassengerSuccessfully()
        {
            UInt64 Id = 2;
            passenger.PassengerId = Id;
            await BizContext.AddPassenger(passenger);

            Passenger newPassenger = await BizContext.GetPassenger(Id);
            Assert.AreEqual(newPassenger.PassengerId, Id);

            string firstName = "TestUpdatedFirstName";
            string middleName = "TestUpdatedMiddleName";
            string lastName = "TestUpdatedLastName";
            string pnr = "testPNR";
            string seatNo = "testSeatNo";
            string flightNo = "testFlightNo";
            string address = "TestAddress";
            string phoneNo = "TestPhone";
            string email = "TestEmail";
            string remarks = "TestRemarks";
            bool subscribed = true;

            newPassenger.PassengerFirstName = firstName;
            newPassenger.PassengerMiddleName = middleName;
            newPassenger.PassengerLastName = lastName;
            newPassenger.PNR = pnr;
            newPassenger.SeatNumber = seatNo;
            newPassenger.FlightNumber = flightNo;
            newPassenger.Address = address;
            newPassenger.Phone = phoneNo;
            newPassenger.EMail = email;
            newPassenger.Remarks = remarks;
            newPassenger.Subscribed = subscribed;

            List<Luggage> luggages = new List<Luggage>();
            luggages.Add(new Luggage(Guid.NewGuid().ToString()));
            luggages.Add(new Luggage(Guid.NewGuid().ToString()));

            newPassenger.Luggages = luggages;


            await BizContext.UpdatePassenger(newPassenger);
            Passenger updatedPassenger = await DALContext.GetPassenger(Id);
            Assert.AreEqual(Id, updatedPassenger.PassengerId);
            Assert.AreEqual(firstName, updatedPassenger.PassengerFirstName);
            Assert.AreEqual(lastName, updatedPassenger.PassengerLastName);
            Assert.AreEqual(middleName, updatedPassenger.PassengerMiddleName);
            Assert.AreEqual(pnr, updatedPassenger.PNR);
            Assert.AreEqual(seatNo, updatedPassenger.SeatNumber);
            Assert.AreEqual(flightNo, updatedPassenger.FlightNumber);
            Assert.AreEqual(address, updatedPassenger.Address);
            Assert.AreEqual(phoneNo, updatedPassenger.Phone);
            Assert.AreEqual(remarks, updatedPassenger.Remarks);
            Assert.AreEqual(subscribed, updatedPassenger.Subscribed);
            Assert.AreEqual(2, updatedPassenger.Luggages.Count);
        }

        [TestMethod]
        public async Task ShouldGetRightNoOfLuggages()
        {
            UInt64 Id = 3;
            passenger.PassengerId = Id;
            await BizContext.AddPassenger(passenger);

            Passenger newPassenger = await BizContext.GetPassenger(Id);

            List<Luggage> luggages = new List<Luggage>();
            luggages.Add(new Luggage(Guid.NewGuid().ToString()));
            luggages.Add(new Luggage(Guid.NewGuid().ToString()));

            newPassenger.Luggages = luggages;
            Assert.AreEqual(newPassenger.Luggages.Count, 2);
        }

        [TestMethod]
        public async Task ShouldGetRightNoOfPassengers()
        {
            string pnr = "TEST";
            await BizContext.AddPassenger(new Passenger(5, "TEST", "FirstName1", "LastName1"));
            await BizContext.AddPassenger(new Passenger(6, "TEST", "FirstName2", "LastName2"));

            List<Passenger> newPassengerList = await BizContext.GetPassengers(pnr);

            Assert.AreEqual(newPassengerList.Count, 2);
        }


        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddNullLuggage()
        {
            await BizContext.AddLuggage(null);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddNullPassenger()
        {
            await BizContext.AddPassenger(null);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddPassengerWithBlankPNR()
        {
            passenger.PNR = "";
            await BizContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddPassengerWithNullPNR()
        {
            passenger.PNR = null;
            await BizContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddPassengerWithBlankPassengerFirstName()
        {
            passenger.PassengerFirstName = "";
            await BizContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddPassengerWithNullPassengerFirstName()
        {
            passenger.PassengerFirstName = null;
            await BizContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddPassengerWithBlankPassengerLastName()
        {
            passenger.PassengerLastName = "";
            await BizContext.AddPassenger(passenger);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddPassengerWithNullPassengerLastName()
        {
            passenger.PassengerLastName = null;
            await BizContext.AddPassenger(passenger);
        }

    }
}
