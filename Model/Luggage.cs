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

        public Luggage(string tagId)
        {
            this.LuggageId = tagId;
        }

    }

    public enum LuggageStatus
    {
        CheckedIn,
        BoardedOnFlight
    }
}