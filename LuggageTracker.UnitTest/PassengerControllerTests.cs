namespace LuggageTracker.UnitTest
{
    using LuggageTracker.BL;
    using LuggageTracker.Model;
    using LuggageTrackerWebRole.Controllers;
    using LugggeTracker.DAL;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestClass]
    public class PassengerControllerTests
    {
        IBL BizContext;
        Passenger passenger = null;
        PassengerController passengerController;


        UInt64 passengerId = 696969;
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
        List<Luggage> luggages = new List<Luggage> { new Luggage(Guid.NewGuid().ToString(),"LuggageName") };



        [TestInitialize]
        public void Initialize()
        {
            BizContext = new LuggageTrackerBizContext(new InMemDAL());

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

            passengerController = new PassengerController(BizContext);
        }

        [TestMethod]
        public async Task ShouldAddPassengerSuccessfully()
        {
            passengerController.ControllerContext = new ControllerContext();
            HttpResponseMessage createResult = await passengerController.AddPassenger(passenger);
                        
            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created,"Verify passenger created");

            HttpResponseMessage getResult = await passengerController.GetPassenger(passengerId);

            Assert.AreEqual(getResult.StatusCode, HttpStatusCode.OK, "verify passenger get");

            string json = await getResult.Content.ReadAsStringAsync();

            Passenger newPassenger = JsonConvert.DeserializeObject<Passenger>(json);

            Assert.AreEqual(newPassenger.PassengerId, passengerId);

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
            Assert.AreEqual(newPassenger.Luggages.Count, 1);
        }

        [TestMethod]
        public async Task ShouldUpdatePassengerSuccessfully()
        {
            passengerId = 69696969;
            passengerFirstName = "Updated_TestFirstName";
            passengerMiddleName = "Updated_TestMiddleName";
            passengerLastName = "Updated_TestLastName";
            pnr = Guid.NewGuid().ToString();
            flightNo = "Updated_TestFlightNo";
            seatNo = "Updated_TestSeatNo";
            address = "Updated_TestAddress";
            phone = "Updated_TestPhone";
            email = "Updated_TestEmail";
            remarks = "Updated_TestRremarks";
            subscribed = false;
            luggages.Add(new Luggage(Guid.NewGuid().ToString(), "LuggageName"));

            passenger.PassengerId = passengerId;
            HttpResponseMessage createResult = await passengerController.AddPassenger(passenger);
            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created, "Verify passenger created");


            passenger.PassengerFirstName = passengerFirstName;
            passenger.PassengerLastName = passengerLastName;
            passenger.PassengerMiddleName = passengerMiddleName;
            passenger.PNR = pnr;
            passenger.FlightNumber = flightNo;
            passenger.SeatNumber = seatNo;
            passenger.Address = address;
            passenger.Phone = phone;
            passenger.EMail = email;
            passenger.Remarks = remarks;
            passenger.Subscribed = subscribed;
            passenger.Luggages = luggages;

            passengerController.ControllerContext = new ControllerContext();
            HttpResponseMessage updatedResult = await passengerController.UpdatePassenger(passenger);

            Assert.AreEqual(updatedResult.StatusCode, HttpStatusCode.OK, "Verify passenger modified");

            HttpResponseMessage getResult = await passengerController.GetPassenger(passengerId);

            Assert.AreEqual(getResult.StatusCode, HttpStatusCode.OK, "verify passenger get");

            string json = await getResult.Content.ReadAsStringAsync();

            Passenger newPassenger = JsonConvert.DeserializeObject<Passenger>(json);

            Assert.AreEqual(newPassenger.PassengerId, passengerId);

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
            Assert.AreEqual(newPassenger.Luggages.Count, 2);
        }

        [TestMethod]
        public async Task ShouldGetAllPassengersForPNRSuccessfully()
        {
            passengerController.ControllerContext = new ControllerContext();
            passenger.PNR = pnr;
            HttpResponseMessage createResult = await passengerController.AddPassenger(passenger);

            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created, "Verify passenger created");
            
            passengerId = 6969692;
            passengerFirstName = "Second_TestFirstName";
            passengerLastName = "Second_TestLastName";
            Passenger passenger2 = new Passenger(passengerId, pnr, passengerFirstName, passengerLastName);
            HttpResponseMessage createResult2 = await passengerController.AddPassenger(passenger2);

            Assert.AreEqual(createResult2.StatusCode, HttpStatusCode.Created, "Verify 2nd passenger created");

            HttpResponseMessage getResult = await passengerController.GetPassengers(pnr);

            Assert.AreEqual(getResult.StatusCode, HttpStatusCode.OK, "verify passengers get for pnr");

            string json = await getResult.Content.ReadAsStringAsync();

            List<Passenger> allPassengers = JsonConvert.DeserializeObject<List<Passenger>>(json);

            Assert.AreEqual(allPassengers.Count, 2);
            Assert.AreEqual(allPassengers[0].PNR, allPassengers[1].PNR);
        }

    }
}
