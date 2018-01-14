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
            catch (ArgumentException ex)
            {
                throw new LuggageTrackerBizContextException("Invalid Argument", ex)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to update luggage", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task UpdateLuggageStatus(string luggageId, string luggageStatus)
        {
            try
            {
                Luggage luggage = await DAL.GetLuggage(luggageId);
                LuggageStatus status = (LuggageStatus) Enum.Parse(typeof(LuggageStatus), luggageStatus, true);

                if (luggage == null)
                {
                    throw new ArgumentException("No such luggage to update");
                }
                else
                {
                    luggage.Status = status;
                    luggage.LastStatusChange = DateTime.Now;
                    DAL.UpdateLuggage(luggage);
                }
            }
            catch (ArgumentException ex)
            {
                throw new LuggageTrackerBizContextException("Invalid Argument", ex)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            catch (Exception ex)
            {
                throw new LuggageTrackerBizContextException("Failed to update luggage status", ex)
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task AddPassenger(Passenger passenger)
        {
            try
            {
                Validator.ValidatePassengerOrThrowException(passenger, isNewPassenger: true);
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
            catch (ArgumentException ex)
            {
                throw new LuggageTrackerBizContextException("Invalid Argument", ex)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };
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
                    throw new ArgumentNullException("Tag Id", "Invalid TagId");
                }

                Luggage luggage = await DAL.GetLuggage(tagId);

                if (luggage is null)
                {
                    throw new NullReferenceException("null object was returned for this parameter");
                }

                return luggage;
            }
            catch (NullReferenceException ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get luggage with TagId", ex)
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
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
                List<Luggage> luggages = await DAL.GetLuggages(passengerId);

                if(luggages is null || luggages.Count < 1)
                {
                    throw new NullReferenceException("null object was returned for this parameter");
                }

                return luggages;
            }
            catch (NullReferenceException ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get luggages with PassengerId", ex)
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
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
                Passenger passenger = await DAL.GetPassenger(passengerId);

                if (passenger is null)
                {
                    throw new NullReferenceException("null object was returned for this parameter");
                }

                return passenger;
            }
            catch (NullReferenceException ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get passenger with PassengerId", ex)
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
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
                    throw new ArgumentNullException("pnr", "Invalid pnr");
                }

                List<Passenger> passengers = await DAL.GetPassengers(pnr);

                if (passengers is null || passengers.Count < 1)
                {
                    throw new NullReferenceException("null object was returned for this parameter");
                }

                return passengers;
            }
            catch (NullReferenceException ex)
            {
                throw new LuggageTrackerBizContextException("Failed to get passenger with PNR", ex)
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
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
