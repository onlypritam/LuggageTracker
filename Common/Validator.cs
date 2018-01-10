using LuggageTracker.Model;
using System;

namespace LuggageTracker.Common
{
    public static class Validator
    {
        public static void ValidatePassengerOrThrowException(Passenger passenger, bool newPassenger = false)
        {
            if (passenger is null)
            {
                throw new ArgumentNullException("Passenger", "Passenger cannot be null");
            }

            if (!newPassenger &&  passenger.PassengerId == null)
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

        public static void ValidateLuggageOrThrowException(Luggage luggage)
        {
            if (luggage is null)
            {
                throw new ArgumentNullException("Luggage","Luggage cannnot be null");
            }

            if (string.IsNullOrWhiteSpace(luggage.LuggageId))
            {
                throw new ArgumentException("Invalid TagId");
            }
        }

    }
}
