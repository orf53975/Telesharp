using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
	/// <summary>
	///	 This object represent a user's profile pictures.
	/// </summary>
	public class UserProfilePhotos
	{
		/// <summary>
		///	 Total number of profile pictures the target user has
		/// </summary>
		[JsonProperty(PropertyName = "total_count")]
		public int TotalCount { get; set; }

		/// <summary>
		///	 Requested profile pictures (in up to 4 sizes each)
		/// </summary>
		[JsonProperty(PropertyName = "photos")]
		public PhotoSize[][] Photos { get; set; } = {};
	}
}