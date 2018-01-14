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
    [Route("taggageservice/v1/luggage")]
    public class LuggageController : Controller
    {
        private IDAL DataContext;
        private IBL BizContext;

        public LuggageController(IDAL dataContext, IBL bizContext)
        {
            this.DataContext = dataContext;
            this.BizContext = bizContext;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddLuggage()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            Luggage luggage = null;
            string json;

            try
            {
                using (MemoryStream requestBodyStream = new MemoryStream())
                {
                    await Request.Body.CopyToAsync(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    json = await new StreamReader(requestBodyStream).ReadToEndAsync();
                }

                luggage = JsonConvert.DeserializeObject<Luggage>(json);
                Validator.ValidateLuggageOrThrowException(luggage);

                await BizContext.AddLuggage(luggage);
                response = new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent("Luggage added successfully") };
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
        public async Task<HttpResponseMessage> UpdateLuggage()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            Luggage luggage = null;
            string json;

            try
            {
                using (MemoryStream requestBodyStream = new MemoryStream())
                {
                    await Request.Body.CopyToAsync(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    json = await new StreamReader(requestBodyStream).ReadToEndAsync();
                }

                luggage = JsonConvert.DeserializeObject<Luggage>(json);
                Validator.ValidateLuggageOrThrowException(luggage);

                await BizContext.UpdateLuggage(luggage);
                response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Luggage updated successfully") };
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
        [Route("{luggageId}/{status}")]
        public async Task<HttpResponseMessage> UpdateLuggageStatus(string luggageId, string status)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                await BizContext.UpdateLuggageStatus(luggageId, status);
                response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Luggage status updated successfully") };
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
        [Route("{tagId}")]
        public async Task<HttpResponseMessage> GetLuggage(string tagId)
        {
            Luggage luggage = null;

            if (string.IsNullOrWhiteSpace(tagId))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("tagId is required") };
            }

            try
            {
                luggage = await BizContext.GetLuggage(tagId);

            }
            catch (LuggageTrackerBizContextException ex)
            {
                return new HttpResponseMessage(ex.StatusCode) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(luggage), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        [Route("{passengerId}")]
        public async Task<HttpResponseMessage> GetLuggages(UInt64 passengerId)
        {
            List<Luggage> luggages = null;

            try
            {
                luggages = await BizContext.GetLuggages(passengerId);

            }
            catch (LuggageTrackerBizContextException ex)
            {
                return new HttpResponseMessage(ex.StatusCode) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(luggages), System.Text.Encoding.UTF8, "application/json") };
        }
    }
}