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

        public void AddLuggage(string tagId, string weight, string measurement, string description, LuggageStatus status, DateTime lastStatusChange)
        {
            try
            {
                Luggages.Add(new Luggage(tagId) {Weight = weight,
                                                Measurement = measurement,
                                                Description = description,
                                                Status = status,
                                                LastStatusChange = lastStatusChange});
            }
            catch
            {
                throw;
            }
        }

        public void UpdateLuggage(string tagId, string weight, string measurement, string description, LuggageStatus status, DateTime lastStatusChange)
        {
            try
            {

                Luggage luggage = Luggages.Single(e => e.TagId == tagId);

                luggage.Weight = weight;
                luggage.Measurement = measurement;
                luggage.Description = description;
                luggage.Status = status;
                luggage.LastStatusChange = lastStatusChange;
            }
            catch
            {
                throw;
            }
        }

        public void AddPassenger(UInt64 passengerId, string pnr, string passengerFirstName, string passengerMiddleName, string passengerLastName, string flightNumber, string seatNumber, string address, string Phone, string eMail, string remarks, List<Luggage> luggages)
        {
            try
            {
                Passengers.Add(new Passenger(passengerId, pnr,passengerFirstName,passengerLastName){PassengerMiddleName = passengerMiddleName,
                                                                        FlightNumber = flightNumber,
                                                                        SeatNumber = seatNumber,
                                                                        Address = address,
                                                                        Phone = Phone,
                                                                        EMail = eMail,
                                                                        Remarks = remarks,
                                                                        Luggages = luggages});
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePassenger(UInt64 passengerId, string pnr, string passengerFirstName, string passengerMiddleName, string passengerLastName, string flightNumber, string seatNumber, string address, string phone, string eMail, string remarks, List<Luggage> luggages)
        {
            try
            {
                Passenger passenger = Passengers.Single(e => e.PassengerId == passengerId);

                passenger.PNR = pnr;
                passenger.PassengerFirstName = passengerFirstName;
                passenger.PassengerLastName = passengerLastName;
                passenger.PassengerMiddleName = passengerMiddleName;
                passenger.FlightNumber = flightNumber;
                passenger.SeatNumber = seatNumber;
                passenger.Address = address;
                passenger.Phone = phone;
                passenger.EMail = eMail;
                passenger.Remarks = remarks;
                passenger.Luggages = luggages;

            }
            catch
            {
                throw;
            }
        }

        public Luggage GetLuggage(string tagId)
        {
            Luggage luggage = null;
            try
            {
                luggage = Luggages.Single(e => e.TagId == tagId);
                return luggage;
            }
            catch
            {
                throw;
            }
        }

        public List<Luggage> GetLuggages(string pnr)
        {
            List<Luggage> luggages = null;
            try
            {
                //luggages = Passengers.Where(e => e.PNR == pnr).ToList();
                return luggages;
            }
            catch
            {
                throw;
            }
        }

        public Passenger GetPassenger(UInt64 passengerId)
        {
            Passenger passenger = null;
            try
            {
                passenger = Passengers.Single(e => e.PassengerId == passengerId);
                return passenger;
            }
            catch
            {
                throw;
            }
        }

        public List<Passenger> GetPassengers(string pnr)
        {
            List<Passenger> passengers = null;
            try
            {
                passengers = Passengers.Where(e => e.PNR == pnr).ToList();
                return passengers;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateLuggage(string TagId, string Weight, string Measurement, string Description, LuggageStatus Status, DateTime LastStatusChange)
        {
            throw new NotImplementedException();
        }

       
    }
}
