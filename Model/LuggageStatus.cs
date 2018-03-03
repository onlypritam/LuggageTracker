namespace LuggageTracker.Model
{
    using System;

    [Serializable]
    public class LuggageStatus
    {
        public UInt64 LuggageStatusId { get; set; } //Unique

        public LuggageStatusEnum Status { get; set; }

        public string StatusDescription { get; set; }

        public GeoLocation Location { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public LuggageStatus(LuggageStatusEnum status)
        {
            this.Status = status;
        }
    }

    [Serializable]
    public class GeoLocation
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
