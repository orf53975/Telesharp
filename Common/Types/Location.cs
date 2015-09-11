using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class Location
    {
        public Location() : this(0, 0)
        {
        }

        public Location(float longitude, float latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        ///     Longitude as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "longitude")]
        public float Longitude { get; set; }

        /// <summary>
        ///     Latitude as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "latitude")]
        public float Latitude { get; set; }
    }
}