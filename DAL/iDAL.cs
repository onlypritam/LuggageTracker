using LuggageTracker.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LugggeTracker.DAL
{
    interface IDAL
    {
        void AddPassenger(Passenger passenger);

        void UpdatePassenger(Passenger passenger);

        void AddLuggage(Luggage luggage);

        void UpdateLuggage(Luggage luggage);

        Luggage GetLuggage(string tagId);

        List<Luggage> GetLuggages(UInt64 passengerId);

        Passenger GetPassenger(UInt64 passengerId);

        List<Passenger> GetPassengers(string pnr);
    }
}
