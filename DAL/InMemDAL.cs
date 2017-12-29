using LuggageTracker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LugggeTracker.DAL
{
    public class InMemDAL : IDAL
    {
        List<Luggage> Luggages = new List<Luggage>();
        List<Passenger> Passengers = new List<Passenger>();

        public async Task AddLuggage(Luggage luggage)
        {
            try
            {
                if(luggage is null)
                {
                    throw new ArgumentNullException("Luggage cannnot be null");
                }

                if (string.IsNullOrWhiteSpace(luggage.TagId))
                {
                    throw new ArgumentException("Invalid TagId");
                }
                
                await Task.Run(() => Luggages.Add(luggage));
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateLuggage(Luggage luggage)
        {
            try
            {
                if (luggage is null)
                {
                    throw new ArgumentNullException("Luggage cannnot be null");
                }

                if (string.IsNullOrWhiteSpace(luggage.TagId))
                {
                    throw new ArgumentException("Invalid TagId");
                }


                Luggage thisLuggage = await Task.Run(()=> Luggages.Single(e => e.TagId == luggage.TagId));

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

        public async Task AddPassenger(Passenger passenger)
        {
            try
            {
                if(passenger is null)
                {
                    throw new ArgumentNullException("Passenger cannot be null");
                }
                
                if(passenger.PassengerId < 1 )
                {
                    throw new ArgumentException("PassengerId cannot be less than 1");
                }

                if (string.IsNullOrWhiteSpace(passenger.PNR))
                {
                    throw new ArgumentException("Invalid PNR");
                }

                if (string.IsNullOrWhiteSpace(passenger.PassengerFirstName))
                {
                    throw new ArgumentException("Invalid PassengerFirstName");
                }

                if (string.IsNullOrWhiteSpace(passenger.PassengerLastName))
                {
                    throw new ArgumentException("Invalid PassengerLastName");
                }


                await Task.Run(() => Passengers.Add(passenger));
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdatePassenger(Passenger passenger)
        {
            try
            {
                if (passenger is null)
                {
                    throw new ArgumentNullException("Passenger cannot be null");
                }

                if (passenger.PassengerId < 1)
                {
                    throw new ArgumentException("PassengerId cannot be less than 1");
                }

                if (string.IsNullOrWhiteSpace(passenger.PNR))
                {
                    throw new ArgumentException("Invalid PNR");
                }

                if (string.IsNullOrWhiteSpace(passenger.PassengerFirstName))
                {
                    throw new ArgumentException("Invalid PassengerFirstName");
                }

                if (string.IsNullOrWhiteSpace(passenger.PassengerLastName))
                {
                    throw new ArgumentException("Invalid PassengerLastName");
                }

                Passenger thisPassenger = await Task.Run(() =>Passengers.Single(e => e.PassengerId == passenger.PassengerId));

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

        public async Task<Luggage> GetLuggage(string tagId)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(tagId))
                {
                    throw new ArgumentNullException("Invalid TagId");
                }
                
                Luggage luggage = await Task.Run(() => Luggages.Single(e => e.TagId == tagId));
                return luggage;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Luggage>> GetLuggages(UInt64 passengerId)
        {
            try
            {
                List<Luggage> luggages = await Task.Run(() => Passengers.Single(e => e.PassengerId == passengerId).Luggages);

                return luggages;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Passenger> GetPassenger(UInt64 passengerId)
        {
            try
            {
                Passenger  passenger = await Task.Run(() =>  Passengers.Single(e => e.PassengerId == passengerId));
                return passenger;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Passenger>> GetPassengers(string pnr)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(pnr))
                {
                    throw new ArgumentNullException("Invalid PNR");
                }
                
                List<Passenger> passengers = await Task.Run(() => Passengers.Where(e => e.PNR == pnr).ToList());
                return passengers;
            }
            catch
            {
                throw;
            }
        }              
    }
}
