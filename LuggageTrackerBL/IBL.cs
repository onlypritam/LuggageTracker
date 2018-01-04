namespace LuggageTracker.BL
{
    using LuggageTracker.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBL
    {
        Task AddPassenger(Passenger passenger);

        Task UpdatePassenger(Passenger passenger);

        Task AddLuggage(Luggage luggage);

        Task UpdateLuggage(Luggage luggage);

        Task<Luggage> GetLuggage(string tagId);

        Task<List<Luggage>> GetLuggages(UInt64 passengerId);

        Task<Passenger> GetPassenger(UInt64 passengerId);

        Task<List<Passenger>> GetPassengers(string pnr);
    }
}
