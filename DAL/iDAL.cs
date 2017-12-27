using LuggageTracker.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LugggeTracker.DAL
{
    interface IDAL
    {

        void AddPassenger(string PNR,
                            string PassengerFirstName, 
                            string PassengerMiddleName,
                            string PassengerLastName,
                            string FlightNumber,
                            string SeatNumber,
                            string Address,
                            string Phone,
                            string EMail,
                            string Remarks,
                            List<Luggage> Luggages);

        void UpdatePassenger(string PNR,
                            string PassengerFirstName,
                            string PassengerMiddleName,
                            string PassengerLastName,
                            string FlightNumber,
                            string SeatNumber,
                            string Address,
                            string Phone,
                            string EMail,
                            string Remarks,
                            List<Luggage> Luggages);

        void AddLuggage(string TagId,
                            string Weight,
                            string Measurement,
                            string Description,
                            LuggageStatus Status,
                            DateTime LastStatusChange);

        void UpdateLuggage(string TagId,
                            string Weight,
                            string Measurement,
                            string Description,
                            LuggageStatus Status,
                            DateTime LastStatusChange);

        List<Luggage> GetLuggages(string PNR);

        Luggage GetLuggage(string TagId);

        List<Passenger> GetPassengers(string PNR);

    }
}
