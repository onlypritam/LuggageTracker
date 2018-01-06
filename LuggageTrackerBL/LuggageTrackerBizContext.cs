namespace LuggageTracker.BL
{
    using LuggageTracker.Common;
    using LuggageTracker.Model;
    using LugggeTracker.DAL;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// For now this layer is not doing much in addition to the DAL. But I am just keeping a provision to add functionality in the future.
    /// </summary>

    public class LuggageTrackerBizContext : IBL
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
                Validator.ValidateLuggageOrThrowException(luggage);
                await  DAL.AddLuggage(luggage);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to add luggage", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task UpdateLuggage(Luggage luggage)
        {
            try
            {
                Validator.ValidateLuggageOrThrowException(luggage);
                if(DAL.GetLuggage(luggage.LuggageId) == null)
                {
                    throw new ArgumentException("No such luggage to update");
                }
                await DAL.UpdateLuggage(luggage);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to update luggage", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task AddPassenger(Passenger passenger)
        {
            try
            {
                Validator.ValidatePassengerOrThrowException(passenger, newPassenger: true);
                await DAL.AddPassenger(passenger);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to add passenger", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task UpdatePassenger(Passenger passenger)
        {
            try
            {
                Validator.ValidatePassengerOrThrowException(passenger);

                if (DAL.GetPassenger(passenger.PassengerId.GetValueOrDefault()) == null)
                {
                    throw new ArgumentException("No such passenger to update");
                }
                await DAL.UpdatePassenger(passenger);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to update passenger", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Luggage> GetLuggage(string tagId)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(tagId))
                {
                    throw new ArgumentNullException("Invalid TagId");
                }

                return await DAL.GetLuggage(tagId);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get luggage with TagId", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<List<Luggage>> GetLuggages(UInt64 passengerId)
        {
            try
            {
                return await DAL.GetLuggages(passengerId);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get luggages with PassengerId", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Passenger> GetPassenger(UInt64 passengerId)
        {
            try
            {
                return await DAL.GetPassenger(passengerId);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get passenger with PassengerId", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<List<Passenger>> GetPassengers(string pnr)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(pnr))
                {
                    throw new ArgumentNullException("Invalid pnr");
                }

                return await DAL.GetPassengers(pnr);
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get passengers with PNR", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

    }
}
