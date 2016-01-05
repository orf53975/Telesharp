using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
	/// <summary>
	///	 This object represents a point on the map.
	/// </summary>
	public class Location
	{
		/// <summary>
		///	 Location constructor
		/// </summary>
		public Location() : this(0, 0)
		{
		}

		/// <summary>
		///	 Location constructor
		/// </summary>
		/// <param name="longitude">Longitude as defined by sender</param>
		/// <param name="latitude">Latitude as defined by sender</param>
		public Location(float longitude, float latitude)
		{
			Longitude = longitude;
			Latitude = latitude;
		}

		/// <summary>
		///	 Longitude as defined by sender
		/// </summary>
		[JsonProperty(PropertyName = "longitude")]
		public float Longitude { get; set; }

		/// <summary>
		///	 Latitude as defined by sender
		/// </summary>
		[JsonProperty(PropertyName = "latitude")]
		public float Latitude { get; set; }
	}
}