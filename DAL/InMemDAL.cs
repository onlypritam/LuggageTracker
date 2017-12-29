using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuggageTracker.Common;

namespace LugggeTracker.DAL
{
    class InMemDAL : IDAL
    {
        List<Luggage> Luggages = new List<Luggage>();
        List<Passenger> Passengers = new List<Passenger>();

        public void AddLuggage(Luggage luggage)
        {
            try
            {
                if(luggage is null)
                {
                    throw new ArgumentNullException();
                }
                
                if(string.IsNullOrWhiteSpace(luggage.TagId))
                {
                    throw new ArgumentException();
                {
                
                Luggages.Add(luggage);
            }
            catch
            {
                throw;
            }
        }

        public void UpdateLuggage(Luggage luggage)
        {
            try
            {
                 if(luggage is null)
                {
                    throw new ArgumentNullException();
                }
                
                if(string.IsNullOrWhiteSpace(luggage.TagId))
                {
                    throw new ArgumentException();
                {
                
                Luggage thisLuggage = Luggages.Single(e => e.TagId == luggage.TagId);

                thisLuggage.Weight = luggage.Weight;
                thisLuggage.Measurement = luggage.Measurement;
                thisLuggage.Description = luggage.Description;
                thisLuggage.Status = luggage.Status;
                thisLuggage.LastStatusChange = luggage.LastStatusChange;
            }
            catch
            {
                throw;
            }
        }

        public void AddPassenger(Passenger passenger)
        {
            try
            {
                if(passenger is null)
                {
                    throw new ArgumentNullException();
                }
                
                if(passenger.PassengerId < 1 or 
                        string.IsNullOrWhiteSpace(passenger.PNR) or 
                        string.IsNullOrWhiteSpace(passenger.PassengerFirstName) or
                        string.IsNullOrWhiteSpace(passenger.PassengerLastName))
                {
                    throw new ArgumentException();
                {
                
                Passengers.Add(passenger);
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePassenger(Passenger passenger)
        {
            try
            {
                if(passenger is null)
                {
                    throw new ArgumentNullException();
                }
                
                if(passenger.PassengerId < 1 or 
                        string.IsNullOrWhiteSpace(passenger.PNR) or 
                        string.IsNullOrWhiteSpace(passenger.PassengerFirstName) or
                        string.IsNullOrWhiteSpace(passenger.PassengerLastName))
                {
                    throw new ArgumentException();
                {
                
                Passenger thisPassenger = Passengers.Single(e => e.PassengerId == passenger.PassengerId);

                thisPassenger.PNR = passenger.PNR;
                thisPassenger.PassengerFirstName = passenger.PassengerFirstName;
                thisPassenger.PassengerLastName = passenger.PassengerLastName;
                thisPassenger.PassengerMiddleName = passenger.PassengerMiddleName;
                thisPassenger.FlightNumber = passenger.FlightNumber;
                thisPassenger.SeatNumber = passenger.SeatNumber;
                thisPassenger.Address = passenger.Address;
                thisPassenger.Phone = passenger.Phone;
                thisPassenger.EMail = passenger.EMail;
                thisPassenger.Remarks = passenger.Remarks;
                thisPassenger.Subscribed = passenger.Subscribed;
                thisPassenger.Luggages = passenger.Luggages;
            }
            catch
            {
                throw;
            }
        }

        public Luggage GetLuggage(string tagId)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(tagId))
                {
                    throw new ArgumentNullException();
                }
                
                Luggage luggage = Luggages.Single(e => e.TagId == tagId);
                return luggage;
            }
            catch
            {
                throw;
            }
        }

        public List<Luggage> GetLuggages(UInt64 passengerId)
        {
            try
            {
                List<Luggage> luggages = Passengers.Single(e => e.PassengerId == passengerId).Luggages;

                return luggages;
            }
            catch
            {
                throw;
            }
        }

        public Passenger GetPassenger(UInt64 passengerId)
        {
            try
            {
                Passenger  passenger = Passengers.Single(e => e.PassengerId == passengerId);
                return passenger;
            }
            catch
            {
                throw;
            }
        }

        public List<Passenger> GetPassengers(string pnr)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(pnr))
                {
                    throw new ArgumentNullException();
                }
                
                List<Passenger> passengers = Passengers.Where(e => e.PNR == pnr).ToList();
                return passengers;
            }
            catch
            {
                throw;
            }
        }              
    }
}
