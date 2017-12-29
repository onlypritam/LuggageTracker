using System;
using System.Collections.Generic;
using System.Text;

namespace LuggageTracker.Common
{
    public class Luggage
    {

        public string TagId { get; set; }

        public string Weight { get; set; }

        public string Measurement { get; set; }

        public string Description { get; set; }

        public LuggageStatus Status { get; set; }

        public DateTime LastStatusChange { get; set; }

        public Luggage(string tagId)
        {
            this.TagId = tagId;
        }

    }

    public enum LuggageStatus
    {
        CheckedIn,
        BoardedOnFlight
    }
}