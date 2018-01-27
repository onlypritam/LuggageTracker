namespace LuggageTracker.Model
{
    using System;

    [Serializable]
    class LuggageStatus
    {
        public UInt64 LuggageStatusId { get; set; } //Unique

        public LuggageStatusEnum Status { get; set; }

        public string Description { get; set; }

        public GeoLocation Location { get; set; }

        public DateTime DateTimeStamp { get; set; }
    }

    [Serializable]
    class GeoLocation
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public GeoLocation(string latitude, string longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
