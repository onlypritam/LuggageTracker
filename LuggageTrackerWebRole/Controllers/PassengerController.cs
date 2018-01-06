using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LuggageTracker.BL;
using LuggageTracker.Common;
using LuggageTracker.Model;
using LugggeTracker.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LuggageTrackerWebRole.Controllers
{
    [Produces("application/json")]
    [Route("LuggageTracker/Passenger")]
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
        [Route("")]
        public async Task<HttpResponseMessage> AddPassenger()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            Passenger passenger = null;
            string json;

            try
            {
                using (MemoryStream requestBodyStream = new MemoryStream())
                {
                    await Request.Body.CopyToAsync(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    json = await new StreamReader(requestBodyStream).ReadToEndAsync();
                }

                passenger = JsonConvert.DeserializeObject<Passenger>(json);
                Validator.ValidatePassengerOrThrowException(passenger, newPassenger: true);

                await BizContext.AddPassenger(passenger);

                return response;
            }
            catch (LuggageTrackerBizContextException ex)
            {
                if (ex.InnerException is ArgumentException || ex.InnerException is ArgumentNullException)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        [Route("")]
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

                return response;
            }
            catch (LuggageTrackerBizContextException ex)
            {
                if (ex.InnerException is ArgumentException || ex.InnerException is ArgumentNullException)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> UpdatePassenger()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            Passenger passenger = null;
            string json;

            try
            {
                using (MemoryStream requestBodyStream = new MemoryStream())
                {
                    await Request.Body.CopyToAsync(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    json = await new StreamReader(requestBodyStream).ReadToEndAsync();
                }

                passenger = JsonConvert.DeserializeObject<Passenger>(json);
                Validator.ValidatePassengerOrThrowException(passenger, newPassenger: false);

                await BizContext.UpdatePassenger(passenger);

                return response;
            }
            catch (LuggageTrackerBizContextException ex)
            {
                if (ex.InnerException is ArgumentException || ex.InnerException is ArgumentNullException)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        [Route("")]
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

                return response;
            }
            catch (LuggageTrackerBizContextException ex)
            {
                if (ex.InnerException is ArgumentException || ex.InnerException is ArgumentNullException)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }


    }
}