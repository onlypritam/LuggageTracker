using LuggageTracker.Model;
using LugggeTracker.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuggageTracker.UnitTest
{
    [TestClass]
    public class DALTests
    {

        IDAL DALContext;
        Luggage luggage = null;
        Passenger passenger = null;
        LuggageStatus luggageStatus = null;

        //Initial Passenger property values
        UInt64 passengerId = 6969;
        string passengerFirstName = "TestFirstName";
        string passengerMiddleName = "TestMiddleName";
        string passengerLastName = "TestLastName";
        string pnr = Guid.NewGuid().ToString();
        string flightNo = "TestFlightNo";
        string seatNo = "TestSeatNo";
        string address = "TestAddress";
        string phone = "TestPhone";
        string email = "TestEmail";
        string remarks = "TestRremarks";
        bool subscribed = true;
        List<Luggage> luggages = new List<Luggage> { new Luggage(Guid.NewGuid().ToString(), "LuggageName") };


        //Initial luggage property values
        string luggageId = Guid.NewGuid().ToString();
        string name = "Test luggage name";
        string weight = "Test luggage weight";
        string measurement = "Test luggage measurement";
        string description = "Test luggage description";
        DateTime lastStatusChange = DateTime.Now;

        //Initial Luggage status property values
        UInt64 luggageStatusId = 12345;
        private LuggageStatusEnum status = LuggageStatusEnum.Registered;
        string testStatusDescription = "Test Status Description";
        string latitude = "0";
        string longitude = "0";
               
        

        [TestInitialize]
        public void Initialize()
        {
            DALContext = new InMemDAL();

            luggageStatus = new LuggageStatus(status)
            {
                LuggageStatusId = luggageStatusId,
                StatusDescription = testStatusDescription,
                Location= new GeoLocation(latitude,longitude),
                DateTimeStamp = lastStatusChange,
            };

            luggage = new Luggage(luggageId,name)
            {
                Weight = weight,
                Measurement = measurement,
                Description = description,
                Status = luggageStatus,
                LastStatusChange = lastStatusChange,
            };

            passenger = new Passenger(passengerId, pnr, passengerFirstName, passengerLastName)
            {
                PassengerMiddleName = passengerMiddleName,
                FlightNumber = flightNo,
                SeatNumber = seatNo,
                Address = address,
                Phone = phone,
                EMail = email,
                Remarks = remarks,
                Subscribed = subscribed,
                Luggages = luggages,
            };
        }

    #region LuggageTests

        [TestMethod]
        public async Task ShouldAddLuggageSuccessfully()
        {
            string tag = Guid.NewGuid().ToString();
            luggage.LuggageId = tag;

            await DALContext.AddLuggage(luggage);

            Luggage newLuggage = await DALContext.GetLuggage(tag);

            Assert.AreEqual(tag, newLuggage.LuggageId);
            Assert.AreEqual(weight, newLuggage.Weight);
            Assert.AreEqual(measurement, newLuggage.Measurement);
            Assert.AreEqual(name, newLuggage.Name);
            Assert.AreEqual(description, newLuggage.Description);
            Assert.AreEqual(lastStatusChange, newLuggage.LastStatusChange);

            Assert.AreEqual(testStatusDescription, newLuggage.Status.StatusDescription);
            Assert.AreEqual(status, newLuggage.Status.Status);
            Assert.AreEqual(luggageStatusId, newLuggage.Status.LuggageStatusId);
            Assert.AreEqual(longitude, newLuggage.Status.Location.Longitude);
            Assert.AreEqual(latitude, newLuggage.Status.Location.Latitude);
            Assert.AreEqual(lastStatusChange, newLuggage.Status.DateTimeStamp);
        }

        [TestMethod]
        public async Task ShouldUpdateLuggageSuccessfully()
        {
            string tag = Guid.NewGuid().ToString();
            luggage.LuggageId = tag;
            await DALContext.AddLuggage(luggage);

            Luggage newLuggage = await DALContext.GetLuggage(tag);
            Assert.AreEqual(tag, newLuggage.LuggageId);

            string description = "TestLuggage";
            string weight = "1 KG";
            string name = "TestName";
            string measurement = "1 Meters";
            DateTime lastStatusChange = DateTime.Now;

            UInt64 statusId = 123456;
            string statusDescription = "Test Status Description Updated";
            string latitude = "1";
            string longitude = "1";


            newLuggage.Description = description;
            newLuggage.Weight = weight;
            newLuggage.Name = name;
            newLuggage.Measurement = measurement;
            newLuggage.LastStatusChange = lastStatusChange;

            newLuggage.Status.LuggageStatusId = statusId;
            newLuggage.Status.Status = LuggageStatusEnum.CheckedIn;
            newLuggage.Status.StatusDescription = statusDescription;
            newLuggage.Status.Location.Latitude = latitude;
            newLuggage.Status.Location.Longitude = longitude;


            await DALContext.UpdateLuggage(newLuggage);
            Luggage updatedLuggage = await DALContext.GetLuggage(tag);

            Assert.AreEqual(tag, updatedLuggage.LuggageId);
            Assert.AreEqual(description, updatedLuggage.Description);
            Assert.AreEqual(weight, updatedLuggage.Weight);
            Assert.AreEqual(name, updatedLuggage.Name);
            Assert.AreEqual(measurement, updatedLuggage.Measurement);
            Assert.AreEqual(lastStatusChange, updatedLuggage.LastStatusChange);

            Assert.AreEqual(statusId, newLuggage.Status.LuggageStatusId);
            Assert.AreEqual(LuggageStatusEnum.CheckedIn,(object)newLuggage.Status.Status);
            Assert.AreEqual(statusDescription, newLuggage.Status.StatusDescription);
            Assert.AreEqual(latitude, newLuggage.Status.Location.Latitude);
            Assert.AreEqual(longitude, newLuggage.Status.Location.Longitude);
        }

        [TestMethod]
        public async Task ShouldGetRightNoOfLuggages()
        {
            UInt64 Id = 3;
            passenger.PassengerId = Id;
            await DALContext.AddPassenger(passenger);

            Passenger newPassenger = await DALContext.GetPassenger(Id);

            List<Luggage> luggages = new List<Luggage>();
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));

            newPassenger.Luggages = luggages;
            Assert.AreEqual(newPassenger.Luggages.Count, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ShouldFailToAddNullLuggage()
        {
            await DALContext.AddLuggage(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToAddLuggageWithBlankName()
        {
            await DALContext.AddLuggage(new Luggage("TestId",""));
        }

        [TestMethod]
        public async Task ShouldAddBlankLuggageIdLuggage()
        {
            luggage.LuggageId = "";
            await DALContext.AddLuggage(luggage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToUpdateLuggageWithBlankName()
        {
            await DALContext.UpdateLuggage(new Luggage("TestId", ""));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ShouldFailToUpdateNullLuggage()
        {
            await DALContext.UpdateLuggage(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ShouldFailToUpdateBlankLuggageIdLuggage()
        {
            await DALContext.UpdateLuggage(new Luggage("", "LuggageName"));
        }
        #endregion

    #region PassengerTest
        [TestMethod]
        public async Task ShouldAddPassengerSuccessfully()
        {
            UInt64 Id = 1;
            passenger.PassengerId = Id;
            await DALContext.AddPassenger(passenger);

            Passenger newPassenger = await DALContext.GetPassenger(Id);
            Assert.AreEqual(newPassenger.PassengerId, Id);

            Assert.AreEqual(newPassenger.PassengerFirstName, passengerFirstName);
            Assert.AreEqual(newPassenger.PassengerMiddleName, passengerMiddleName);
            Assert.AreEqual(newPassenger.PassengerLastName, passengerLastName);
            Assert.AreEqual(newPassenger.PNR, pnr);
            Assert.AreEqual(newPassenger.SeatNumber, seatNo);
            Assert.AreEqual(newPassenger.FlightNumber, flightNo);
            Assert.AreEqual(newPassenger.Address, address);
            Assert.AreEqual(newPassenger.EMail, email);
            Assert.AreEqual(newPassenger.Phone, phone);
            Assert.AreEqual(newPassenger.Remarks, remarks);
            Assert.AreEqual(newPassenger.Subscribed, subscribed);
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
        public async Task ShouldUpdatePassengerSuccessfully()
        {
            UInt64 Id = 2;
            passenger.PassengerId = Id;
            await DALContext.AddPassenger(passenger);

            Passenger newPassenger = await DALContext.GetPassenger(Id);
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
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));

            newPassenger.Luggages = luggages;


            await DALContext.UpdatePassenger(newPassenger);
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
        public async Task ShouldGetRightNoOfPassengers()
        {
            string pnr = "TEST";
            await DALContext.AddPassenger(new Passenger(5,"TEST","FirstName1","LastName1"));
            await DALContext.AddPassenger(new Passenger(6, "TEST", "FirstName2", "LastName2"));
            
            List<Passenger> newPassengerList = await DALContext.GetPassengers(pnr);

            Assert.AreEqual(newPassengerList.Count, 2);
        }
    #endregion

    }
}
