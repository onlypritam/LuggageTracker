using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LuggageTracker.BL;
using LugggeTracker.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            try
            {

                //await BizContext.AddPassenger();
                using (var requestBodyStream = new MemoryStream())
                {
                    var body = Request.Body;
                    await Request.Body.CopyToAsync(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    string message = await new StreamReader(requestBodyStream).ReadToEndAsync();
                }

                return new HttpResponseMessage();

            }
            catch
            {
                throw;
            }

        }


    }
}