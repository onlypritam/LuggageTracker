using System;

namespace LuggageTracker.Model
{
    public class Luggage
    {

        public string LuggageId { get; set; }

        public string Name { get; set; }

        public string Weight { get; set; }

        public string Measurement { get; set; }

        public string Description { get; set; }

        public LuggageStatus Status { get; set; }

        public DateTime LastStatusChange { get; set; }

        public Luggage(string luggageId)
        {
            this.LuggageId = luggageId;
        }

    }

    public enum LuggageStatus
    {
        Registered,
        CheckedIn,
        BoardedOnFlight
    }
}