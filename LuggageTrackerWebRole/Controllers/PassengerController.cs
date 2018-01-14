namespace LuggageTrackerWebRole.Controllers
{
    using LuggageTracker.BL;
    using LuggageTracker.Common;
    using LuggageTracker.Model;
    using LugggeTracker.DAL;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("taggageservice/v1/passenger")]
    public class PassengerController : Controller
    {
        private IDAL DataContext;
        private IBL BizContext;

        public PassengerController(IDAL dataContext, IBL bizContext)
        {
            this.DataContext = dataContext;
            this.BizContext = bizContext;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddPassenger([FromBody] Passenger passenger)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
           // Passenger passenger = null;
            //string json;

            try
            {
                //using (MemoryStream requestBodyStream = new MemoryStream())
                //{
                //    await Request.Body.CopyToAsync(requestBodyStream);
                //    requestBodyStream.Seek(0, SeekOrigin.Begin);
                //    json = await new StreamReader(requestBodyStream).ReadToEndAsync();
                //}

                //passenger = JsonConvert.DeserializeObject<Passenger>(json);
                Validator.ValidatePassengerOrThrowException(passenger, isNewPassenger: true);

                await BizContext.AddPassenger(passenger);
                response = new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent("Passenger added successfully") };

            }
            catch (LuggageTrackerBizContextException ex)
            {
                response = new HttpResponseMessage(ex.StatusCode) { Content = new StringContent(ex.Message) };
            }
            catch (ArgumentException ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

            return response;
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePassenger([FromBody] Passenger passenger)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //Passenger passenger = null;
            //string json;

            try
            {
                //using (MemoryStream requestBodyStream = new MemoryStream())
                //{
                //    await Request.Body.CopyToAsync(requestBodyStream);
                //    requestBodyStream.Seek(0, SeekOrigin.Begin);
                //    json = await new StreamReader(requestBodyStream).ReadToEndAsync();
                //}

                //passenger = JsonConvert.DeserializeObject<Passenger>(json);
                Validator.ValidatePassengerOrThrowException(passenger, isNewPassenger: false);

                await BizContext.UpdatePassenger(passenger);
                response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Passenger updated successfully") };
            }
            catch (LuggageTrackerBizContextException ex)
            {
                response = new HttpResponseMessage(ex.StatusCode) { Content = new StringContent(ex.Message) };
            }
            catch (ArgumentException ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

            return response;
        }
      
        [HttpGet]
        [Route("{passengerId}")]
        public async Task<HttpResponseMessage> GetPassenger(UInt64 passengerId)
        {
            Passenger passenger = null;

            try
            {
                passenger = await BizContext.GetPassenger(passengerId);

            }
            catch (LuggageTrackerBizContextException ex)
            {
                return new HttpResponseMessage(ex.StatusCode) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(passenger), System.Text.Encoding.UTF8, "application/json") };

        }

        [HttpGet]
        [Route("{pnr}")]
        public async Task<HttpResponseMessage> GetPassengers(string pnr)
        {
            List<Passenger> passengers = null;

            if (string.IsNullOrWhiteSpace(pnr))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("pnr is required") };
            }

            try
            {
                passengers = await BizContext.GetPassengers(pnr);

            }
            catch (LuggageTrackerBizContextException ex)
            {
                return new HttpResponseMessage(ex.StatusCode) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(passengers), System.Text.Encoding.UTF8, "application/json") };

        }
    }
}