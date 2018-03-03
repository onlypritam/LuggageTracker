using System;

namespace LuggageTracker.Model
{
    [Serializable]
    public class Luggage
    {

        public string LuggageId { get; set; } //Unique

        public string Name { get; set; }

        public string Weight { get; set; }

        public string Measurement { get; set; }

        public string Description { get; set; }

        public LuggageStatus Status { get; set; }

        public DateTime LastStatusChange { get; set; }

        public Luggage(string luggageId, string name)
        {
            this.LuggageId = luggageId;
            this.Name = name;
        }

    }

    public enum LuggageStatusEnum
    {
        Registered,
        CheckedIn,
        BoardedOnFlight
    }
}