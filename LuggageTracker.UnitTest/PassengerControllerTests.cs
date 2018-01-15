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
        PassengerController controller;


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
        List<Luggage> luggages = new List<Luggage> { new Luggage(Guid.NewGuid().ToString()) };



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

            controller = new PassengerController(BizContext);
        }

        [TestMethod]
        public async Task ShouldAddPassengerSuccessfully()
        {
            controller.ControllerContext = new ControllerContext();
            HttpResponseMessage createResult = await controller.AddPassenger(passenger);
                        
            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created,"Verify passenger created");

            HttpResponseMessage getResult = await controller.GetPassenger(passengerId);

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


        }
    }
}
