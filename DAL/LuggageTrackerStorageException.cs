namespace DAL.Exceptions
{
    using System;


    [Serializable]
    class LuggageTrackerStorageException: Exception
    {

        public LuggageTrackerStorageException()
        {

        }

        public LuggageTrackerStorageException(string message): base(message)
        {

        }

        public LuggageTrackerStorageException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
