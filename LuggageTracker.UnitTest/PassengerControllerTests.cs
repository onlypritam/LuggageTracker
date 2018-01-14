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
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestClass]
    public class PassengerControllerTests
    {
        IDAL DALContext;
        IBL BizContext;
        Luggage luggage = null;
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
            DALContext = new InMemDAL();

            BizContext = new LuggageTrackerBizContext(DALContext);

            //luggage = new Luggage(Guid.NewGuid().ToString());

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

            controller = new PassengerController(DALContext, BizContext);
        }

        [TestMethod]
        public async Task ShouldAddPassengerSuccessfully()
        {
            controller.ControllerContext = new ControllerContext();

            //controller.AddPassenger(passenger);
            //controller.ControllerContext.HttpContext = new DefaultHttpContext();
            //controller.ControllerContext.HttpContext.Request  = new HttpRequestMessage()
            //{
            //    Content = new StringContent(JsonConvert.SerializeObject(passenger))
            //};

            //controller.Request = new HttpRequestMessage()
            //{
            //    Content = new StringContent(JsonConvert.SerializeObject(passenger))
            //};

        }
    }
}
