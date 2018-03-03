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
       // LuggageStatus status = new LuggageStatus(LuggageStatusEnum.Registered);
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

            BizContext = new LuggageTrackerBizContext(DALContext);

            luggageStatus = new LuggageStatus(status)
            {
                LuggageStatusId = luggageStatusId,
                StatusDescription = testStatusDescription,
                Location = new GeoLocation(latitude, longitude),
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
            await BizContext.AddLuggage(luggage);

            Luggage newLuggage = await BizContext.GetLuggage(tag);

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
            await BizContext.AddLuggage(luggage);

            Luggage newLuggage = await BizContext.GetLuggage(tag);
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

            await BizContext.UpdateLuggage(newLuggage);
            Luggage updatedLuggage = await DALContext.GetLuggage(tag);

            Assert.AreEqual(tag, updatedLuggage.LuggageId);
            Assert.AreEqual(description, updatedLuggage.Description);
            Assert.AreEqual(weight, updatedLuggage.Weight);
            Assert.AreEqual(name, updatedLuggage.Name);
            Assert.AreEqual(measurement, updatedLuggage.Measurement);
            Assert.AreEqual(lastStatusChange, updatedLuggage.LastStatusChange);

            Assert.AreEqual(statusId, newLuggage.Status.LuggageStatusId);
            Assert.AreEqual(LuggageStatusEnum.CheckedIn, (object)newLuggage.Status.Status);
            Assert.AreEqual(statusDescription, newLuggage.Status.StatusDescription);
            Assert.AreEqual(latitude, newLuggage.Status.Location.Latitude);
            Assert.AreEqual(longitude, newLuggage.Status.Location.Longitude);
        }

        [TestMethod]
        public async Task ShouldUpdateLuggageStatusSuccessfully()
        {
            LuggageStatus status = new LuggageStatus(LuggageStatusEnum.Registered);
            status.LuggageStatusId = 1234;
            status.StatusDescription = "Old Status description";
            status.Location = new GeoLocation("111", "222");
            status.DateTimeStamp = DateTime.MaxValue;


            LuggageStatus UpdatedStatus = new LuggageStatus(LuggageStatusEnum.CheckedIn);
            UpdatedStatus.StatusDescription = "Status description";
            UpdatedStatus.LuggageStatusId = 123;
            UpdatedStatus.Location = new GeoLocation("11", "22");
            UpdatedStatus.DateTimeStamp = DateTime.MinValue;


            string tag = Guid.NewGuid().ToString();

            luggage.LuggageId = tag;
            luggage.Status = status;
            await BizContext.AddLuggage(luggage);

            await BizContext.UpdateLuggageStatus(tag, UpdatedStatus);

            Luggage updatedLuggage = await BizContext.GetLuggage(tag);

            Assert.AreEqual(tag, updatedLuggage.LuggageId);
            Assert.AreEqual("Status description", updatedLuggage.Status.StatusDescription);
            Assert.AreEqual((UInt64)123, updatedLuggage.Status.LuggageStatusId);
            Assert.AreEqual("11", updatedLuggage.Status.Location.Latitude);
            Assert.AreEqual("22", updatedLuggage.Status.Location.Longitude);
            Assert.AreEqual(DateTime.MinValue, updatedLuggage.Status.DateTimeStamp);
            Assert.AreEqual(LuggageStatusEnum.CheckedIn, (object)updatedLuggage.Status.Status);
        }
        
        [TestMethod]
        public async Task ShouldGetRightNoOfLuggagesUsingPassengerId()
        {
            UInt64 Id = 3;
            passenger.PassengerId = Id;
            await BizContext.AddPassenger(passenger);

            Passenger newPassenger = await BizContext.GetPassenger(Id);

            List<Luggage> luggages = new List<Luggage>();
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));

            newPassenger.Luggages = luggages;
            Assert.AreEqual(newPassenger.Luggages.Count, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToUpdateLuggageWithoutValidLuggageId()
        {
            Luggage luggage = new Luggage(Guid.NewGuid().ToString(), "LuggageName");
            await BizContext.UpdateLuggage(luggage);
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToUpdateLuggageWithBlankName()
        {
            await BizContext.UpdateLuggage(new Luggage("TestId", ""));
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddLuggageWithBlankName()
        {
            await BizContext.AddLuggage(new Luggage("TestId", ""));
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToUpdateLuggageWithBlankLuggageId()
        {
            await BizContext.UpdateLuggage(new Luggage("", "LuggageName"));
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldAddLuggageWithBlankLuggageIdSuccessfully()
        {
            await BizContext.AddLuggage(new Luggage("", "LuggageName"));
        }

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToAddNullLuggage()
        {
            await BizContext.AddLuggage(null);
        }

    #endregion

    #region PassengerTests
        [TestMethod]
        public async Task ShouldAddPassengerSuccessfully()
        {
            UInt64 Id = 1;
            passenger.PassengerId = Id;
            await BizContext.AddPassenger(passenger);

            Passenger newPassenger = await BizContext.GetPassenger(Id);
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
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));

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

        [TestMethod]
        [ExpectedException(typeof(LuggageTrackerBizContextException))]
        public async Task ShouldFailToUpdatePassengerWithoutValidPassengerId()
        {
            Passenger passenger = new Passenger(99, "XXX", "FirstName", "LastName");
            await BizContext.UpdatePassenger(passenger);
        }

    #endregion

    }
}
