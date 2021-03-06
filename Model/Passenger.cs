﻿namespace LuggageTracker.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Passenger
    {
        public UInt64? PassengerId { get; set; } //Unique

        public string PNR { get; set; }

        public string PassengerFirstName { get; set; }

        public string PassengerMiddleName { get; set; }

        public string PassengerLastName { get; set; }

        public string FlightNumber { get; set; }

        public string SeatNumber { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string EMail { get; set; }

        public string Remarks { get; set; }

        public bool Subscribed { get; set; }

        public List<Luggage> Luggages { get; set; }

        public Passenger(UInt64 passengerId,string pnr, string passengerFirstName, string passengerLastName)
        {
            this.PassengerId = passengerId;
            this.PNR = pnr;
            this.PassengerFirstName = passengerFirstName;
            this.PassengerLastName = passengerLastName;
        }

    }
}
