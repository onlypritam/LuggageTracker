using LuggageTracker.Model;
using LuggageTrackerBL.Exceptions;
using LugggeTracker.DAL;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LuggageTrackerBL.BizContext
{
    class LuggageTrackerBizContext
    {
        private IDAL DAL = null;

        public LuggageTrackerBizContext(IDAL dal)
        {
            this.DAL = dal;
        }


        public async Task AddLuggage(Luggage luggage)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to add luggage to storage", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task UpdateLuggage(Luggage luggage)
        {
            try
            {
                //Validator.ValidateLuggage(luggage);
            }
            catch
            {
                throw;
            }
        }

    }
}
