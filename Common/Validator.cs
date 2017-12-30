using LuggageTracker.Model;
using System;

namespace LuggageTracker.Common
{
    public static class Validator
    {
        public static void ValidatePassenger(Passenger passenger)
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
        }

        public static void ValidateLuggage(Luggage luggage)
        {
            if (luggage is null)
            {
                throw new ArgumentNullException("Luggage cannnot be null");
            }

            if (string.IsNullOrWhiteSpace(luggage.LuggageId))
            {
                throw new ArgumentException("Invalid TagId");
            }
        }

    }
}
