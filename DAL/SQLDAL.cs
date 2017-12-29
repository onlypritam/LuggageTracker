using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LuggageTracker.Common;

namespace LugggeTracker.DAL
{
    public class SQLDAL : IDAL
    {
        public Task AddLuggage(Luggage luggage)
        {
            throw new NotImplementedException();
        }

        public Task AddPassenger(Passenger passenger)
        {
            throw new NotImplementedException();
        }

        public Task<Luggage> GetLuggage(string tagId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Luggage>> GetLuggages(ulong passengerId)
        {
            throw new NotImplementedException();
        }

        public Task<Passenger> GetPassenger(ulong passengerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Passenger>> GetPassengers(string pnr)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLuggage(Luggage luggage)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePassenger(Passenger passenger)
        {
            throw new NotImplementedException();
        }
    }
}
