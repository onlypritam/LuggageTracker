namespace LuggageTracker.BL
{
    using System;
    using System.Net;

    [Serializable]
    public class LuggageTrackerBizContextException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public LuggageTrackerBizContextException() : this(HttpStatusCode.InternalServerError)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
        }

        public LuggageTrackerBizContextException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public LuggageTrackerBizContextException(string message) : base(message)
        {

        }

        public LuggageTrackerBizContextException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
