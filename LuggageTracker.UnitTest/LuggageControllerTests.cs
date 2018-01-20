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
    public class LuggageControllerTests
    {
        IBL BizContext;
        Luggage luggage = null;
        LuggageController luggageController;
        PassengerController passengerController;


        string luggageId = Guid.NewGuid().ToString();
        string name = "Name";
        string weight = "Weight";
        string measurement = "Measurement";
        string description = "Description";
        LuggageStatus luggageStatus = LuggageStatus.CheckedIn;
        DateTime lastStatusChange = DateTime.Now;
        

        [TestInitialize]
        public void Initialize()
        {
            BizContext = new LuggageTrackerBizContext(new InMemDAL());

            luggage = new Luggage(luggageId,name)
            {
                Weight = weight,
                Measurement = measurement,
                Description = description,
                Status = luggageStatus,
                LastStatusChange = lastStatusChange,
            };
            
            luggageController = new LuggageController(BizContext);
            passengerController = new PassengerController(BizContext);
        }

        [TestMethod]
        public async Task ShouldAddLuggageSuccessfully()
        {
            luggageController.ControllerContext = new ControllerContext();
            HttpResponseMessage createResult = await luggageController.AddLuggage(luggage);

            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created, "Verify luggage created");

            HttpResponseMessage getResult = await luggageController.GetLuggage(luggageId);

            Assert.AreEqual(getResult.StatusCode, HttpStatusCode.OK, "verify luggage get");

            string json = await getResult.Content.ReadAsStringAsync();

            Luggage newLuggage = JsonConvert.DeserializeObject<Luggage>(json);

            Assert.AreEqual(newLuggage.LuggageId, luggageId);
            Assert.AreEqual(newLuggage.Name, name);
            Assert.AreEqual(newLuggage.Weight, weight);
            Assert.AreEqual(newLuggage.Measurement, measurement);
            Assert.AreEqual(newLuggage.Description, description);
            Assert.AreEqual(newLuggage.Status, luggageStatus);
            Assert.AreEqual(newLuggage.LastStatusChange, lastStatusChange);
        }

        [TestMethod]
        public async Task ShouldUpdateLuggageSuccessfully()
        {
            luggageId = Guid.NewGuid().ToString();
            name = "Updated_Name";
            weight = "Updates_Weight";
            measurement = "Updates_Measurement";
            description = "Updates_Description";
            luggageStatus = LuggageStatus.Registered;
            lastStatusChange = DateTime.Now;

            luggage.LuggageId = luggageId;
            HttpResponseMessage createResult = await luggageController.AddLuggage(luggage);
            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created, "Verify luggage created");


            luggage.Name = name;
            luggage.Weight = weight;
            luggage.Measurement = measurement;
            luggage.Description = description;
            luggage.Status = luggageStatus;
            luggage.LastStatusChange = lastStatusChange;
            
            luggageController.ControllerContext = new ControllerContext();
            HttpResponseMessage updatedResult = await luggageController.UpdateLuggage(luggage);

            Assert.AreEqual(updatedResult.StatusCode, HttpStatusCode.OK, "Verify luggage modified");

            HttpResponseMessage getResult = await luggageController.GetLuggage(luggageId);

            Assert.AreEqual(getResult.StatusCode, HttpStatusCode.OK, "verify luggage get");

            string json = await getResult.Content.ReadAsStringAsync();

            Luggage newLuggage = JsonConvert.DeserializeObject<Luggage>(json);

            Assert.AreEqual(newLuggage.LuggageId, luggageId);
            Assert.AreEqual(newLuggage.Name, name);
            Assert.AreEqual(newLuggage.Weight, weight);
            Assert.AreEqual(newLuggage.Measurement, measurement);
            Assert.AreEqual(newLuggage.Description, description);
            Assert.AreEqual(newLuggage.Status, luggageStatus);
            Assert.AreEqual(newLuggage.LastStatusChange, lastStatusChange);
        }

        [TestMethod]
        public async Task ShouldUpdateLuggageStatusSuccessfully()
        {
            string luggageStringStatus = "Registered";
            
            luggage.LuggageId = luggageId;
            HttpResponseMessage createResult = await luggageController.AddLuggage(luggage);
            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created, "Verify luggage created");

            luggage.Status = luggageStatus;

            luggageController.ControllerContext = new ControllerContext();
            HttpResponseMessage updatedResult = await luggageController.UpdateLuggageStatus(luggageId, luggageStringStatus);

            Assert.AreEqual(updatedResult.StatusCode, HttpStatusCode.OK, "Verify luggage modified");

            HttpResponseMessage getResult = await luggageController.GetLuggage(luggageId);

            Assert.AreEqual(getResult.StatusCode, HttpStatusCode.OK, "verify luggage get");

            string json = await getResult.Content.ReadAsStringAsync();

            Luggage newLuggage = JsonConvert.DeserializeObject<Luggage>(json);

            Assert.AreEqual(newLuggage.Status, LuggageStatus.Registered);
        }

        [TestMethod]
        public async Task ShouldGetAllLuggageForPassengerIdSuccessfully()
        {
            luggageController.ControllerContext = new ControllerContext();

            HttpResponseMessage createResult = await luggageController.AddLuggage(luggage);
            Assert.AreEqual(createResult.StatusCode, HttpStatusCode.Created, "Verify luggage created");

            string secondLuggageId = Guid.NewGuid().ToString();
            Luggage luggage2 = new Luggage(secondLuggageId, "LuggageName");
            HttpResponseMessage createResult2 = await luggageController.AddLuggage(luggage2);
            Assert.AreEqual(createResult2.StatusCode, HttpStatusCode.Created, "Verify 2nd luggage created");

            UInt64 passengerId = 6969;
            Passenger passenger = new Passenger(passengerId, Guid.NewGuid().ToString(), "Passenger_First_Name", "Passenger_Last_Name");
            List<Luggage> luggages = new List<Luggage>();
            luggages.Add(luggage);
            luggages.Add(luggage2);
            passenger.Luggages = luggages;

            HttpResponseMessage createResult3 = await passengerController.AddPassenger(passenger);
            Assert.AreEqual(createResult3.StatusCode, HttpStatusCode.Created, "Verify passenger created");
            
            HttpResponseMessage getResult = await passengerController.GetPassenger(passengerId);

            Assert.AreEqual(getResult.StatusCode, HttpStatusCode.OK, "verify passengers get");

            string json = await getResult.Content.ReadAsStringAsync();

            Passenger newPassenger = JsonConvert.DeserializeObject<Passenger>(json);

            Assert.AreEqual(newPassenger.Luggages.Count, 2);
        }

    }
}

