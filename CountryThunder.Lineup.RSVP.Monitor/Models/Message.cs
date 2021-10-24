using System;

namespace CountryThunder.Lineup.RSVP.Monitor.Models
{
    [Serializable]
    public class Message
    {
        public string Value { get; }

        public Message(string value)
        {
            Value = value;
        }
    }
}
